using AppWebDesbloqueos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing;

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
        
        public IActionResult ExportarExcel(DateTime? fechaInicio, DateTime? fechaFin)
        {
            // Obtener productos por rango de fechas
            var desbloqueos = ObtenerDesbloqueosPorRangoFecha(fechaInicio.Value, fechaFin.Value);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Desbloqueos");

                // Agregar encabezados
                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Nombre";
                worksheet.Cell(1, 3).Value = "Fecha de Correo";
                worksheet.Cell(1, 4).Value = "Operación Realizada";
                worksheet.Cell(1, 5).Value = "Fecha de Respuesta";
                worksheet.Cell(1, 6).Value = "Observaciones";
                worksheet.Cell(1, 7).Value = "Lista";
                worksheet.Cell(1, 8).Value = "CN";
                worksheet.Cell(1, 9).Value = "Otras Observaciones";
                worksheet.Cell(1, 10).Value = "Tiempo de Atención";
                worksheet.Cell(1, 11).Value = "Accionesta";
                worksheet.Cell(1, 12).Value = "Usuario de Creación";
                worksheet.Cell(1, 13).Value = "Usuario de Modificación";
                worksheet.Cell(1, 14).Value = "Estado";

                // Llenar datos
                int row = 2;
                foreach (var desbloqueo in desbloqueos)
                {
                    worksheet.Cell(row, 1).Value = desbloqueo.Id;
                    worksheet.Cell(row, 2).Value = desbloqueo.Nombre;
                    worksheet.Cell(row, 3).Value = TryParseDateTimeNullable(desbloqueo.FechaCorreo?.ToString("dd/MM/yyyy"));
                    worksheet.Cell(row, 3).Style.NumberFormat.Format = "dd/mm/yyyy";
                    worksheet.Cell(row, 4).Value = desbloqueo.OperacionRealizada1;
                    worksheet.Cell(row, 5).Value = TryParseDateTimeNullable(desbloqueo.FechaRespuestaDesbloqueo?.ToString("dd/MM/yyyy"));
                    worksheet.Cell(row, 5).Style.NumberFormat.Format = "dd/mm/yyyy";
                    worksheet.Cell(row, 6).Value = desbloqueo.Observaciones;
                    worksheet.Cell(row, 7).Value = desbloqueo.Lista;
                    worksheet.Cell(row, 8).Value = desbloqueo.Cn;
                    worksheet.Cell(row, 9).Value = desbloqueo.Observacion;
                    worksheet.Cell(row, 10).Value = desbloqueo.TiempoDeAtencion;
                    worksheet.Cell(row, 11).Value = desbloqueo.Accionista;
                    worksheet.Cell(row, 12).Value = desbloqueo.UsuarioCreacion;
                    worksheet.Cell(row, 13).Value = desbloqueo.UsuarioModificacion;
                    worksheet.Cell(row, 14).Value = desbloqueo.Estado;
                    row++;
                }

                // Ajustar el ancho de las columnas
                worksheet.Columns().AdjustToContents();

               

                // Guardar en un MemoryStream
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Position = 0; // Volver al inicio del stream

                    var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    var fileName = $"ReporteDesbloqueos-{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";

                    return File(stream.ToArray(), contentType, fileName);
                }
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
