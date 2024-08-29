using AppWebDesbloqueos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace AppWebDesbloqueos.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Index()
        {
            using (SqlConnection con = new(Configuration["ConnectionStrings:conexion"]))
            {
                using (SqlCommand cmd = new("CONSULTAR_USUARIOS", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    SqlDataAdapter da = new(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    List<UsuarioModel> lista = new List<UsuarioModel>();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        lista.Add(new UsuarioModel()
                        {
                            IdUsuario = Convert.ToInt32(dt.Rows[i][0]),
                            UsuarioSistema = dt.Rows[i][1].ToString(),                            
                            Nombre = dt.Rows[i][2].ToString()
                        });
                    }
                    ViewBag.Usuarios = lista;
                    con.Close();
                }
            }
            return View();
        }           
    

        public IConfiguration Configuration { get; }

        public UsuariosController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(UsuarioModel usuario)
        {
            using (SqlConnection con = new(Configuration["ConnectionStrings:conexion"]))
            {
                using (SqlCommand cmd = new("INSERTAR_USUARIOS", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@USUARIO", System.Data.SqlDbType.VarChar).Value = usuario.UsuarioSistema;
                    cmd.Parameters.AddWithValue("@NOMBRE", System.Data.SqlDbType.Int).Value = usuario.Nombre;
                    

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return Redirect("Index");
        }

        // Método GET para cargar la vista de edición con el usuario actual
        public IActionResult Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UsuarioModel usuario = ObtenerUsuarioPorId(id.Value);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // Método POST para actualizar los detalles del usuario
        [HttpPost]
        public IActionResult Editar(int id, UsuarioModel usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool resultado = ActualizarUsuario(usuario);
                if (resultado)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return NotFound();
                }
            }

            return View(usuario);
        }

        // Método para obtener un usuario por su Id
        private UsuarioModel ObtenerUsuarioPorId(int id)
        {
            UsuarioModel usuario = null;

            using (SqlConnection con = new(Configuration["ConnectionStrings:conexion"]))
            {
                using (SqlCommand cmd = new("CONSULTAR_ID_USUARIOS", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", System.Data.SqlDbType.VarChar).Value = id;
                    


                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        usuario = new UsuarioModel
                        {
                            IdUsuario = (int)reader["Id"],
                            UsuarioSistema = reader["Usuario"].ToString(),                            
                            Nombre = reader["Nombre"].ToString()

                        };
                    }
                    reader.Close();                    
                    con.Close();
                }                   
                
            }

            return usuario;
        }

        // Método para actualizar un usuario

        private bool ActualizarUsuario(UsuarioModel usuario)
        {

            using (SqlConnection con = new(Configuration["ConnectionStrings:conexion"]))
            {
                using (SqlCommand cmd = new("EDITAR_USUARIOS", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", System.Data.SqlDbType.VarChar).Value = usuario.IdUsuario;
                    cmd.Parameters.AddWithValue("@USUARIO", System.Data.SqlDbType.VarChar).Value = usuario.UsuarioSistema;
                    cmd.Parameters.AddWithValue("@NOMBRE", System.Data.SqlDbType.Int).Value = usuario.Nombre;


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

            UsuarioModel usuario = ObtenerUsuarioPorId(id.Value);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // Método GET para mostrar la vista de confirmación de eliminación
        public IActionResult Eliminar(int? id)
        {
            if (id == null)
            {
                return NotFound(); // Si no se proporciona un ID, devuelve un error 404
            }

            // Obtener el usuario por su ID
            UsuarioModel usuario = ObtenerUsuarioPorId(id.Value);

            if (usuario == null)
            {
                return NotFound(); // Si no se encuentra el usuario, devuelve un error 404
            }

            return View(usuario); // Muestra la vista de confirmación con los detalles del usuario
        }

        // Método POST para confirmar y realizar la eliminación del usuario
        [HttpPost, ActionName("Eliminar")]
        public IActionResult ConfirmarEliminacion(int id)
        {
            // Llama al método para eliminar el usuario
            bool resultado = EliminarUsuario(id);

            if (resultado)
            {
                return RedirectToAction(nameof(Index)); // Redirige al índice si se eliminó correctamente
            }

            return NotFound(); // Si no se pudo eliminar, devuelve un error 404
        }

        // Método para eliminar un usuario de la base de datos
        private bool EliminarUsuario(int id)
        {
            using(SqlConnection con = new(Configuration["ConnectionStrings:conexion"]))
            {
                using (SqlCommand cmd = new("ELIMINAR_USUARIOS", con))
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
