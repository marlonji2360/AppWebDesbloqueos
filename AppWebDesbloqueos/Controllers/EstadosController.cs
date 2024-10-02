using AppWebDesbloqueos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace AppWebDesbloqueos.Controllers
{
    public class EstadosController : Controller
    {
        public IActionResult Index()
        {
            using (SqlConnection con = new(Configuration["ConnectionStrings:conexion"]))
            {
                using (SqlCommand cmd = new("CONSULTAR_ESTADOS", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    SqlDataAdapter da = new(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    List<EstadoModel> lista = new List<EstadoModel>();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        lista.Add(new EstadoModel()
                        {
                            Id = Convert.ToInt32(dt.Rows[i][0]),
                            NombreEstado = dt.Rows[i][1].ToString()
                        });
                    }
                    ViewBag.Estados = lista;
                    con.Close();
                }
            }
            return View();
        }

        public IConfiguration Configuration { get; }

        public EstadosController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(EstadoModel obs)
        {
            using (SqlConnection con = new(Configuration["ConnectionStrings:conexion"]))
            {
                using (SqlCommand cmd = new("INSERTAR_ESTADOS", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ESTADO", System.Data.SqlDbType.VarChar).Value = obs.NombreEstado;
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
            return View(obs);
        }

        // Método GET para cargar la vista de edición con el usuario actual
        public IActionResult Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EstadoModel obs = ObtenerEstadoPorId(id.Value);

            if (obs == null)
            {
                return NotFound();
            }

            return View(obs);
        }

        // Método POST para actualizar los detalles del usuario
        [HttpPost]
        public IActionResult Editar(int id, EstadoModel obs)
        {
            if (id != obs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool resultado = ActualizarCn(obs);
                if (resultado)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return NotFound();
                }
            }

            return View(obs);
        }

        // Método para obtener un usuario por su Id
        private EstadoModel ObtenerEstadoPorId(int id)
        {
            EstadoModel obs = null;

            using (SqlConnection con = new(Configuration["ConnectionStrings:conexion"]))
            {
                using (SqlCommand cmd = new("CONSULTAR_ID_ESTADOS", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", System.Data.SqlDbType.VarChar).Value = id;



                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        obs = new EstadoModel
                        {
                            Id = (int)reader["Id"],
                            NombreEstado = reader["NOMBRE_ESTADO"].ToString()

                        };
                    }
                    reader.Close();
                    con.Close();
                }

            }

            return obs;
        }

        // Método para actualizar un usuario

        private bool ActualizarCn(EstadoModel obs)
        {

            using (SqlConnection con = new(Configuration["ConnectionStrings:conexion"]))
            {
                using (SqlCommand cmd = new("EDITAR_ESTADOS", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", System.Data.SqlDbType.VarChar).Value = obs.Id;
                    cmd.Parameters.AddWithValue("@ESTADO", System.Data.SqlDbType.VarChar).Value = obs.NombreEstado;
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

            EstadoModel obs = ObtenerEstadoPorId(id.Value);

            if (obs == null)
            {
                return NotFound();
            }

            return View(obs);
        }

        // Método GET para mostrar la vista de confirmación de eliminación
        public IActionResult Eliminar(int? id)
        {
            if (id == null)
            {
                return NotFound(); // Si no se proporciona un ID, devuelve un error 404
            }

            // Obtener el usuario por su ID
            EstadoModel obs = ObtenerEstadoPorId(id.Value);

            if (obs == null)
            {
                return NotFound(); // Si no se encuentra el usuario, devuelve un error 404
            }

            return View(obs); // Muestra la vista de confirmación con los detalles del usuario
        }

        // Método POST para confirmar y realizar la eliminación del usuario
        [HttpPost, ActionName("Eliminar")]
        public IActionResult ConfirmarEliminacion(int id)
        {
            // Llama al método para eliminar el usuario
            bool resultado = EliminarCn(id);

            if (resultado)
            {
                return RedirectToAction(nameof(Index)); // Redirige al índice si se eliminó correctamente
            }

            return NotFound(); // Si no se pudo eliminar, devuelve un error 404
        }

        // Método para eliminar un usuario de la base de datos
        private bool EliminarCn(int id)
        {
            using (SqlConnection con = new(Configuration["ConnectionStrings:conexion"]))
            {
                using (SqlCommand cmd = new("ELIMINAR_ESTADOS", con))
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
