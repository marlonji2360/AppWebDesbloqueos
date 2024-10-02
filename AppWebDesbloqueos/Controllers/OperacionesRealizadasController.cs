using AppWebDesbloqueos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace AppWebDesbloqueos.Controllers
{
    public class OperacionesRealizadasController : Controller
    {
        public IActionResult Index()
        {
            using (SqlConnection con = new(Configuration["ConnectionStrings:conexion"]))
            {
                using (SqlCommand cmd = new("CONSULTAR_OPERACIONES", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    SqlDataAdapter da = new(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    List<OperacionRealizadaModel> lista = new List<OperacionRealizadaModel>();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        lista.Add(new OperacionRealizadaModel()
                        {
                            IdOperacionRealizada = Convert.ToInt32(dt.Rows[i][0]),
                            OperacionRealizada = dt.Rows[i][1].ToString()
                        });
                    }
                    ViewBag.OperacionesRealizadas = lista;
                    con.Close();
                }
            }
            return View();
        }

        public IConfiguration Configuration { get; }

        public OperacionesRealizadasController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(OperacionRealizadaModel operacion)
        {
            using (SqlConnection con = new(Configuration["ConnectionStrings:conexion"]))
            {
                using (SqlCommand cmd = new("INSERTAR_OPERACIONES", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OPERACION", System.Data.SqlDbType.VarChar).Value = operacion.OperacionRealizada;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            if (ModelState.IsValid)
            {
                // Guardar en la base de datos o realizar alguna acción
                return RedirectToAction("Index");
            }

            // Si el modelo no es válido, se vuelve a mostrar el formulario con los mensajes de error
            return View(operacion);
        }

        // Método GET para cargar la vista de edición con el usuario actual
        public IActionResult Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OperacionRealizadaModel operacionRealizada = ObtenerOperacionPorId(id.Value);

            if (operacionRealizada == null)
            {
                return NotFound();
            }

            return View(operacionRealizada);
        }

        // Método POST para actualizar los detalles del usuario
        [HttpPost]
        public IActionResult Editar(int id, OperacionRealizadaModel operacion)
        {
            if (id != operacion.IdOperacionRealizada)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool resultado = ActualizarOperacion(operacion);
                if (resultado)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return NotFound();
                }
            }

            return View(operacion);
        }

        // Método para obtener un usuario por su Id
        private OperacionRealizadaModel ObtenerOperacionPorId(int id)
        {
            OperacionRealizadaModel operacion = null;

            using (SqlConnection con = new(Configuration["ConnectionStrings:conexion"]))
            {
                using (SqlCommand cmd = new("CONSULTAR_ID_OPERACIONES", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", System.Data.SqlDbType.VarChar).Value = id;



                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        operacion = new OperacionRealizadaModel
                        {
                            IdOperacionRealizada = (int)reader["Id"],
                            OperacionRealizada = reader["Operacion"].ToString()

                        };
                    }
                    reader.Close();
                    con.Close();
                }

            }

            return operacion;
        }

        // Método para actualizar un usuario

        private bool ActualizarOperacion(OperacionRealizadaModel operacion)
        {

            using (SqlConnection con = new(Configuration["ConnectionStrings:conexion"]))
            {
                using (SqlCommand cmd = new("EDITAR_OPERACIONES", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", System.Data.SqlDbType.VarChar).Value = operacion.IdOperacionRealizada;
                    cmd.Parameters.AddWithValue("@OPERACION", System.Data.SqlDbType.VarChar).Value = operacion.OperacionRealizada;
                    con.Open();
                    int resultado = cmd.ExecuteNonQuery();
                    con.Close();
                    return resultado > 0; // Si se actualizó al menos una fila
                }
            }
        }

        public IActionResult Detalles(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OperacionRealizadaModel operacion = ObtenerOperacionPorId(id.Value);

            if (operacion == null)
            {
                return NotFound();
            }

            return View(operacion);
        }

        // Método GET para mostrar la vista de confirmación de eliminación
        public IActionResult Eliminar(int? id)
        {
            if (id == null)
            {
                return NotFound(); // Si no se proporciona un ID, devuelve un error 404
            }

            // Obtener el usuario por su ID
            OperacionRealizadaModel operacion = ObtenerOperacionPorId(id.Value);

            if (operacion == null)
            {
                return NotFound(); // Si no se encuentra el usuario, devuelve un error 404
            }

            return View(operacion); // Muestra la vista de confirmación con los detalles del usuario
        }

        // Método POST para confirmar y realizar la eliminación del usuario
        [HttpPost, ActionName("Eliminar")]
        public IActionResult ConfirmarEliminacion(int id)
        {
            // Llama al método para eliminar el usuario
            bool resultado = EliminarOperaciones(id);

            if (resultado)
            {
                return RedirectToAction(nameof(Index)); // Redirige al índice si se eliminó correctamente
            }

            return NotFound(); // Si no se pudo eliminar, devuelve un error 404
        }

        // Método para eliminar un usuario de la base de datos
        private bool EliminarOperaciones(int id)
        {
            using (SqlConnection con = new(Configuration["ConnectionStrings:conexion"]))
            {
                using (SqlCommand cmd = new("ELIMINAR_OPERACIONES", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", System.Data.SqlDbType.VarChar).Value = id;

                    con.Open();
                    int resultado = cmd.ExecuteNonQuery();
                    con.Close();
                    return resultado > 0; // Si se actualizó al menos una fila
                }
            }
        }
    }
}
