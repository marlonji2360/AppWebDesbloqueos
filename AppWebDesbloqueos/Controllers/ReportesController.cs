using AppWebDesbloqueos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using OfficeOpenXml;

namespace AppWebDesbloqueos.Controllers
{
    public class ReportesController : Controller
    {
        public IConfiguration Configuration { get; }

        public ReportesController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult Index(DateTime? fechaInicio, DateTime? fechaFin)
        {
            var desbloqueos = new List<DesbloqueoModel>();

            if (fechaInicio.HasValue && fechaFin.HasValue)
            {
                desbloqueos = ObtenerDesbloqueosPorRangoFecha(fechaInicio.Value, fechaFin.Value);
            }

            ViewBag.FechaInicio = fechaInicio;
            ViewBag.FechaFin = fechaFin;

            return View(desbloqueos);
        }

        // Acción para exportar a Excel
        public IActionResult ExportarExcel(DateTime? fechaInicio, DateTime? fechaFin)
        {
            var desbloqueos = ObtenerDesbloqueosPorRangoFecha(fechaInicio.Value, fechaFin.Value);

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Productos");
                worksheet.Cells["A1"].Value = "ID";
                worksheet.Cells["B1"].Value = "Nombre";
                worksheet.Cells["C1"].Value = "Fecha de Correo";
                worksheet.Cells["D1"].Value = "Operación Realizada";
                worksheet.Cells["E1"].Value = "Fecha de Respuesta";
                worksheet.Cells["F1"].Value = "Observaciones";
                worksheet.Cells["G1"].Value = "Lista";
                worksheet.Cells["H1"].Value = "CN";
                worksheet.Cells["I1"].Value = "Otras Observaciones";
                worksheet.Cells["J1"].Value = "Tiempo de Atención";
                worksheet.Cells["K1"].Value = "Accionesta";
                worksheet.Cells["L1"].Value = "Usuario de Creación";
                worksheet.Cells["M1"].Value = "Usuario de Modificación";
                worksheet.Cells["N1"].Value = "Estado";
                

                int row = 2;
                foreach (var desbloqueo in desbloqueos)
                {
                    worksheet.Cells[row, 1].Value = desbloqueo.Id;
                    worksheet.Cells[row, 2].Value = desbloqueo.Nombre;
                    worksheet.Cells[row, 3].Value = TryParseDateTimeNullable(desbloqueo.FechaCorreo?.ToString("dd/MM/yyyy"));
                    worksheet.Cells[row, 3].Style.Numberformat.Format = "dd/mm/yyyy";
                    worksheet.Cells[row, 4].Value = desbloqueo.OperacionRealizada1;
                    worksheet.Cells[row, 5].Value = TryParseDateTimeNullable(desbloqueo.FechaRespuestaDesbloqueo?.ToString("dd/MM/yyyy"));
                    worksheet.Cells[row, 5].Style.Numberformat.Format = "dd/mm/yyyy";
                    worksheet.Cells[row, 6].Value = desbloqueo.Observaciones;
                    worksheet.Cells[row, 7].Value = desbloqueo.Lista;
                    worksheet.Cells[row, 8].Value = desbloqueo.Cn;
                    worksheet.Cells[row, 9].Value = desbloqueo.Observacion;
                    worksheet.Cells[row, 10].Value = desbloqueo.TiempoDeAtencion;
                    worksheet.Cells[row, 11].Value = desbloqueo.Accionista;
                    worksheet.Cells[row, 12].Value = desbloqueo.UsuarioCreacion;
                    worksheet.Cells[row, 13].Value = desbloqueo.UsuarioModificacion;
                    worksheet.Cells[row, 14].Value = desbloqueo.Estado;
                    row++;
                }

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                string excelName = $"ReporteDesbloqueos-{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";

                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
        }

        // Método para obtener los productos por rango de fecha usando el procedimiento almacenado
        private List<DesbloqueoModel> ObtenerDesbloqueosPorRangoFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            var desbloqueos = new List<DesbloqueoModel>();

            using (SqlConnection con = new(Configuration["ConnectionStrings:conexion"]))
            {
                using (SqlCommand cmd = new("OBTENERDESBLOQUEOSPORFECHA", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@FechaFin", fechaFin);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        desbloqueos.Add(new DesbloqueoModel
                        {
                            Id = (int)reader["Id"],
                            Nombre = reader["Nombre"].ToString(),
                            FechaCorreo = TryParseDateTimeNullable(reader["Fecha_Correo"].ToString()),
                            OperacionRealizada1 = reader["Operacion_Realizada_1"].ToString(),
                            FechaRespuestaDesbloqueo = TryParseDateTimeNullable(reader["Fecha_Respuesta_Desbloqueo"].ToString()),
                            Observaciones = reader["Observaciones"].ToString(),
                            Lista = reader["Lista"].ToString(),
                            Cn = reader["CN"].ToString(),
                            Observacion = reader["Observacion"].ToString(),
                            TiempoDeAtencion = reader["Tiempo_Atencion"].ToString(),
                            Accionista = reader["Accionista"].ToString(),
                            UsuarioCreacion = reader["Usuario_Creacion"].ToString(),
                            UsuarioModificacion = reader["Usuario_Modificacion"].ToString(),                            
                            Estado = reader["Estado"].ToString(),
                        });
                    }

                    reader.Close();
                }
            }

            return desbloqueos;
        }

        private DateTime? TryParseDateTimeNullable(object value)
        {
            DateTime result;
            // Intenta convertir el valor a DateTime, si falla, asigna DateTime.MinValue o un valor que tenga sentido en tu contexto.
            if (DateTime.TryParse(value?.ToString(), out result))
            {
                return result;
            }
            else
            {
                return null; // Puedes cambiar esto a otro valor por defecto si lo prefieres.
            }
        }
    }
}
