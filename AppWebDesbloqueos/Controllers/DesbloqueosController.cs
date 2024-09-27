using AppWebDesbloqueos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;

namespace AppWebDesbloqueos.Controllers
{
    public class DesbloqueosController : Controller
    {
        public IActionResult Index(string buscar, string tipoOrdenamiento)
        {
            if (!string.IsNullOrEmpty(buscar))
            {
                using (SqlConnection con = new(Configuration["ConnectionStrings:conexion"]))
                {
                    using (SqlCommand cmd = new("CONSULTAR_NOMBRE_DESBLOQUEOS", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NOMBRE", System.Data.SqlDbType.VarChar).Value = buscar;
                        con.Open();
                        SqlDataAdapter da = new(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        da.Dispose();
                        List<DesbloqueoModel> lista = new List<DesbloqueoModel>();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            lista.Add(new DesbloqueoModel()
                            {
                                Id = Convert.ToInt32(dt.Rows[i][0]),                                
                                Nombre = dt.Rows[i][1].ToString(),
                                FechaCorreo = TryParseDateTimeNullable(dt.Rows[i][2].ToString()),
                                OperacionRealizada1 = dt.Rows[i][3].ToString(),
                                FechaRespuestaDesbloqueo = TryParseDateTimeNullable(dt.Rows[i][4].ToString()),                                
                                Observaciones = dt.Rows[i][5].ToString(),
                                Lista = dt.Rows[i][6].ToString(),
                                Cn = dt.Rows[i][7].ToString(),
                                Observacion = dt.Rows[i][8].ToString(),
                                TiempoDeAtencion = dt.Rows[i][9].ToString(),
                                Accionista = dt.Rows[i][10].ToString(),                               
                                UsuarioCreacion = dt.Rows[i][11].ToString(),
                                UsuarioModificacion = dt.Rows[i][12].ToString(),
                                FechaCreacion = TryParseDateTimeNullable(dt.Rows[i][13].ToString()),
                                FechaModificacion = TryParseDateTimeNullable(dt.Rows[i][14].ToString()),
                                Estado = dt.Rows[i][15].ToString()

                            });
                        }
                        ViewBag.Desbloqueos = lista;
                    }
                    con.Close();
                }
            }

            if (!string.IsNullOrEmpty(tipoOrdenamiento))
            {

                string storedProcedure = tipoOrdenamiento == "estado_desc" ? "CONSULTAR_DESBLOQUEOS_DES" : "CONSULTAR_DESBLOQUEOS_ASC";



                List<DesbloqueoModel> lista = new List<DesbloqueoModel>();

                using (SqlConnection con = new(Configuration["ConnectionStrings:conexion"]))
                {
                    using (SqlCommand cmd = new(storedProcedure, con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        con.Open();
                        SqlDataAdapter da = new(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        da.Dispose();


                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            lista.Add(new DesbloqueoModel()
                            {
                                Id = Convert.ToInt32(dt.Rows[i][0]),
                                Nombre = dt.Rows[i][1].ToString(),
                                FechaCorreo = TryParseDateTimeNullable(dt.Rows[i][2].ToString()),
                                OperacionRealizada1 = dt.Rows[i][3].ToString(),
                                FechaRespuestaDesbloqueo = TryParseDateTimeNullable(dt.Rows[i][4].ToString()),
                                Observaciones = dt.Rows[i][5].ToString(),
                                Lista = dt.Rows[i][6].ToString(),
                                Cn = dt.Rows[i][7].ToString(),
                                Observacion = dt.Rows[i][8].ToString(),
                                TiempoDeAtencion = dt.Rows[i][9].ToString(),
                                Accionista = dt.Rows[i][10].ToString(),
                                UsuarioCreacion = dt.Rows[i][11].ToString(),
                                UsuarioModificacion = dt.Rows[i][12].ToString(),
                                FechaCreacion = TryParseDateTimeNullable(dt.Rows[i][13].ToString()),
                                FechaModificacion = TryParseDateTimeNullable(dt.Rows[i][14].ToString()),
                                Estado = dt.Rows[i][15].ToString()

                            });
                        }
                        // Parámetro para el cambio de ordenación en la vista

                    }
                    con.Close();
                    ViewBag.Desbloqueos = lista;
                }
            }
            if(string.IsNullOrEmpty(tipoOrdenamiento) && string.IsNullOrEmpty(buscar))
            {
                using (SqlConnection con = new(Configuration["ConnectionStrings:conexion"]))
                {
                    using (SqlCommand cmd = new("CONSULTAR_DESBLOQUEOS", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        con.Open();
                        SqlDataAdapter da = new(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        da.Dispose();
                        List<DesbloqueoModel> lista = new List<DesbloqueoModel>();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            lista.Add(new DesbloqueoModel()
                            {
                                Id = Convert.ToInt32(dt.Rows[i][0]),
                                Nombre = dt.Rows[i][1].ToString(),
                                FechaCorreo = TryParseDateTimeNullable(dt.Rows[i][2].ToString()),
                                OperacionRealizada1 = dt.Rows[i][3].ToString(),
                                FechaRespuestaDesbloqueo = TryParseDateTimeNullable(dt.Rows[i][4].ToString()),
                                Observaciones = dt.Rows[i][5].ToString(),
                                Lista = dt.Rows[i][6].ToString(),
                                Cn = dt.Rows[i][7].ToString(),
                                Observacion = dt.Rows[i][8].ToString(),
                                TiempoDeAtencion = dt.Rows[i][9].ToString(),
                                Accionista = dt.Rows[i][10].ToString(),
                                UsuarioCreacion = dt.Rows[i][11].ToString(),
                                UsuarioModificacion = dt.Rows[i][12].ToString(),
                                FechaCreacion = TryParseDateTimeNullable(dt.Rows[i][13].ToString()),
                                FechaModificacion = TryParseDateTimeNullable(dt.Rows[i][14].ToString()),
                                Estado = dt.Rows[i][15].ToString()

                            });
                        }
                        ViewBag.Desbloqueos = lista;
                    }
                    con.Close();
                }
            }
            return View();

        }

        // Método para obtener un usuario por su Id
        private List<DesbloqueoModel> ObtenerDesbloqueoPorDato(string dato)
        {
            List<DesbloqueoModel> lista = new List<DesbloqueoModel>();
            using (SqlConnection con = new(Configuration["ConnectionStrings:conexion"]))
            {
                using (SqlCommand cmd = new("CONSULTAR_DESBLOQUEOS", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    SqlDataAdapter da = new(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        lista.Add(new DesbloqueoModel()
                        {
                            Id = Convert.ToInt32(dt.Rows[i][0]),
                            Nombre = dt.Rows[i][1].ToString(),
                            FechaCorreo = TryParseDateTimeNullable(dt.Rows[i][2].ToString()),
                            OperacionRealizada1 = dt.Rows[i][3].ToString(),
                            FechaRespuestaDesbloqueo = TryParseDateTimeNullable(dt.Rows[i][4].ToString()),
                            Observaciones = dt.Rows[i][5].ToString(),
                            Lista = dt.Rows[i][6].ToString(),
                            Cn = dt.Rows[i][7].ToString(),
                            Observacion = dt.Rows[i][8].ToString(),
                            TiempoDeAtencion = dt.Rows[i][9].ToString(),
                            Accionista = dt.Rows[i][10].ToString(),
                            UsuarioCreacion = dt.Rows[i][11].ToString(),
                            UsuarioModificacion = dt.Rows[i][12].ToString(),
                            FechaCreacion = TryParseDateTimeNullable(dt.Rows[i][13].ToString()),
                            FechaModificacion = TryParseDateTimeNullable(dt.Rows[i][14].ToString()),
                            Estado = dt.Rows[i][15].ToString()

                        });
                    }
                }
                con.Close();               
            }
            return lista;
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

        public IConfiguration Configuration { get; }

        public DesbloqueosController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult Registrar()
        {
            

            using (SqlConnection con = new(Configuration["ConnectionStrings:conexion"]))
            {
                using (SqlCommand cmd = new("CONSULTAR_CN", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    SqlDataAdapter da = new(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    List<CentroNegocioModel> listaCn = new List<CentroNegocioModel>();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        listaCn.Add(new CentroNegocioModel()
                        {

                            NombreCn = dt.Rows[i][1].ToString()

                        });
                    }
                    ViewBag.Cn = new SelectList(listaCn, "NombreCn", "NombreCn");
                    con.Close();
                }    
            }

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
                    List<EstadoModel> listaEstados = new List<EstadoModel>();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        listaEstados.Add(new EstadoModel()
                        {

                            NombreEstado = dt.Rows[i][1].ToString()

                        });
                    }
                    ViewBag.Estados = new SelectList(listaEstados, "NombreEstado", "NombreEstado");
                    con.Close();
                }
            }

            using (SqlConnection con = new(Configuration["ConnectionStrings:conexion"]))
            {
                using (SqlCommand cmd = new("CONSULTAR_LISTAS", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    SqlDataAdapter da = new(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    List<ListaModel> listaL = new List<ListaModel>();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        listaL.Add(new ListaModel()
                        {

                            NombreLista = dt.Rows[i][1].ToString()

                        });
                    }
                    ViewBag.Listas = new SelectList(listaL, "NombreLista", "NombreLista");
                    con.Close();
                }
            }

            using (SqlConnection con = new(Configuration["ConnectionStrings:conexion"]))
            {
                using (SqlCommand cmd = new("CONSULTAR_OBSERVACIONES", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    SqlDataAdapter da = new(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    List<ObservacionesModel> listaO = new List<ObservacionesModel>();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        listaO.Add(new ObservacionesModel()
                        {

                            ObservacionesDetalle = dt.Rows[i][1].ToString()

                        });
                    }
                    ViewBag.Observaciones = new SelectList(listaO, "ObservacionesDetalle", "ObservacionesDetalle");
                    con.Close();
                }
            }

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
                    List<OperacionRealizadaModel> listaOr = new List<OperacionRealizadaModel>();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        listaOr.Add(new OperacionRealizadaModel()
                        {

                            OperacionRealizada = dt.Rows[i][1].ToString()

                        });
                    }
                    ViewBag.Operaciones = new SelectList(listaOr, "OperacionRealizada", "OperacionRealizada");
                    con.Close();
                }
            }

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
                    List<UsuarioModel> listaU = new List<UsuarioModel>();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        listaU.Add(new UsuarioModel()
                        {

                            UsuarioSistema = dt.Rows[i][1].ToString()

                        });
                    }
                    ViewBag.Usuarios = new SelectList(listaU, "UsuarioSistema", "UsuarioSistema");
                    con.Close();
                }
            }


            return View();
        }

        [HttpPost]
        public IActionResult Registrar(DesbloqueoModel obs)
        {
            using (SqlConnection con = new(Configuration["ConnectionStrings:conexion"]))
            {
                
                using (SqlCommand cmd = new("INSERTAR_DESBLOQUEOS", con))
                {
                    var username = User.Identity.Name;

                    if(username == null)
                    {
                        username = "Pruebas_Bantrab";
                    }

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;                    
                    cmd.Parameters.AddWithValue("@NOMBRE", System.Data.SqlDbType.VarChar).Value = obs.Nombre;
                    cmd.Parameters.AddWithValue("@FECHA_CORREO", System.Data.SqlDbType.DateTime).Value = obs.FechaCorreo;
                    cmd.Parameters.AddWithValue("@OPERACION_REALIZADA_1", System.Data.SqlDbType.VarChar).Value = obs.OperacionRealizada1;
                    cmd.Parameters.AddWithValue("@FECHA_RESPUESTA_DESBLOQUEO", System.Data.SqlDbType.DateTime).Value = obs.FechaRespuestaDesbloqueo;                    
                    cmd.Parameters.AddWithValue("@OBSERVACIONES", obs.Observaciones ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@LISTA", System.Data.SqlDbType.VarChar).Value = obs.Lista;
                    cmd.Parameters.AddWithValue("@CN", System.Data.SqlDbType.VarChar).Value = obs.Cn;
                    cmd.Parameters.AddWithValue("@OBSERVACION", System.Data.SqlDbType.VarChar).Value = obs.Observacion;
                    cmd.Parameters.AddWithValue("@ACCIONISTA", obs.Accionista ?? (object)DBNull.Value);                    
                    cmd.Parameters.AddWithValue("@USUARIO_CREACION", System.Data.SqlDbType.VarChar).Value = username.Substring(8);
                    cmd.Parameters.AddWithValue("@ESTADO", System.Data.SqlDbType.VarChar).Value = obs.Estado;
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

            DesbloqueoModel obs = ObtenerDesbloqueoPorId(id.Value);

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
                    List<EstadoModel> listaEstados = new List<EstadoModel>();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        listaEstados.Add(new EstadoModel()
                        {

                            NombreEstado = dt.Rows[i][1].ToString()

                        });
                    }
                    ViewBag.Estados = new SelectList(listaEstados, "NombreEstado", "NombreEstado");
                    con.Close();
                }
            }

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
                    List<UsuarioModel> listaU = new List<UsuarioModel>();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        listaU.Add(new UsuarioModel()
                        {

                            UsuarioSistema = dt.Rows[i][1].ToString()

                        });
                    }
                    ViewBag.Usuarios = new SelectList(listaU, "UsuarioSistema", "UsuarioSistema");
                    con.Close();
                }
            }

            using (SqlConnection con = new(Configuration["ConnectionStrings:conexion"]))
            {
                using (SqlCommand cmd = new("CONSULTAR_CN", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    SqlDataAdapter da = new(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    List<CentroNegocioModel> listaCn = new List<CentroNegocioModel>();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        listaCn.Add(new CentroNegocioModel()
                        {

                            NombreCn = dt.Rows[i][1].ToString()

                        });
                    }
                    ViewBag.Cns = new SelectList(listaCn, "NombreCn", "NombreCn");
                    con.Close();
                }
            }

            using (SqlConnection con = new(Configuration["ConnectionStrings:conexion"]))
            {
                using (SqlCommand cmd = new("CONSULTAR_LISTAS", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    SqlDataAdapter da = new(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    List<ListaModel> listaL = new List<ListaModel>();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        listaL.Add(new ListaModel()
                        {

                            NombreLista = dt.Rows[i][1].ToString()

                        });
                    }
                    ViewBag.Listas = new SelectList(listaL, "NombreLista", "NombreLista");
                    con.Close();
                }
            }

            using (SqlConnection con = new(Configuration["ConnectionStrings:conexion"]))
            {
                using (SqlCommand cmd = new("CONSULTAR_OBSERVACIONES", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    SqlDataAdapter da = new(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    List<ObservacionesModel> listaO = new List<ObservacionesModel>();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        listaO.Add(new ObservacionesModel()
                        {

                            ObservacionesDetalle = dt.Rows[i][1].ToString()

                        });
                    }
                   
                    ViewBag.Observaciones = new SelectList(listaO, "ObservacionesDetalle", "ObservacionesDetalle");
                    con.Close();
                }
            }

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
                    List<OperacionRealizadaModel> listaOr = new List<OperacionRealizadaModel>();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        listaOr.Add(new OperacionRealizadaModel()
                        {

                            OperacionRealizada = dt.Rows[i][1].ToString()

                        });
                    }
                    ViewBag.Operaciones1 = new SelectList(listaOr, "OperacionRealizada", "OperacionRealizada");
                    ViewBag.Operaciones2 = new SelectList(listaOr, "OperacionRealizada", "OperacionRealizada");
                    con.Close();
                }
            }

            if (obs == null)
            {
                return NotFound();
            }

            return View(obs);
        }

        // Método POST para actualizar los detalles del usuario
        [HttpPost]
        public IActionResult Editar(int id, DesbloqueoModel obs)
        {
            if (id != obs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool resultado = ActualizarDesbloqueo(obs);
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
        private DesbloqueoModel ObtenerDesbloqueoPorId(int id)
        {
            DesbloqueoModel obs = null;

            using (SqlConnection con = new(Configuration["ConnectionStrings:conexion"]))
            {
                using (SqlCommand cmd = new("CONSULTAR_ID_DESBLOQUEOS", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", System.Data.SqlDbType.VarChar).Value = id;



                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        obs = new DesbloqueoModel
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
                            FechaCreacion = TryParseDateTimeNullable(reader["Fecha_Creacion"].ToString()),
                            FechaModificacion = TryParseDateTimeNullable(reader["Fecha_Modificacion"].ToString()),
                            Estado = reader["Estado"].ToString(),

                        };
                    }
                    reader.Close();
                    con.Close();
                }

            }

            return obs;
        }

        // Método para actualizar un usuario

        private bool ActualizarDesbloqueo(DesbloqueoModel obs)
        {

            using (SqlConnection con = new(Configuration["ConnectionStrings:conexion"]))
            {
                using (SqlCommand cmd = new("EDITAR_DESBLOQUEOS", con))
                {
                    var username = User.Identity.Name;

                    if (username == null)
                    {
                        username = "Pruebas_Bantrab";
                    }

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", System.Data.SqlDbType.VarChar).Value = obs.Id;                   
                    cmd.Parameters.AddWithValue("@NOMBRE", System.Data.SqlDbType.VarChar).Value = obs.Nombre;
                    cmd.Parameters.AddWithValue("@FECHA_CORREO", System.Data.SqlDbType.DateTime).Value = obs.FechaCorreo;
                    cmd.Parameters.AddWithValue("@OPERACION_REALIZADA_1", System.Data.SqlDbType.VarChar).Value = obs.OperacionRealizada1;
                    cmd.Parameters.AddWithValue("@FECHA_RESPUESTA_DESBLOQUEO", System.Data.SqlDbType.DateTime).Value = obs.FechaRespuestaDesbloqueo;
                    cmd.Parameters.AddWithValue("@OBSERVACIONES", System.Data.SqlDbType.VarChar).Value = obs.Observaciones;
                    cmd.Parameters.AddWithValue("@LISTA", System.Data.SqlDbType.VarChar).Value = obs.Lista;
                    cmd.Parameters.AddWithValue("@CN", System.Data.SqlDbType.VarChar).Value = obs.Cn;
                    cmd.Parameters.AddWithValue("@OBSERVACION", System.Data.SqlDbType.VarChar).Value = obs.Observacion;
                    cmd.Parameters.AddWithValue("@ACCIONISTA", obs.Accionista ?? (object)DBNull.Value);                 
                    cmd.Parameters.AddWithValue("@USUARIO_MODIFICACION", System.Data.SqlDbType.VarChar).Value = username.Substring(8); ;
                    cmd.Parameters.AddWithValue("@ESTADO", obs.Estado ?? (object)DBNull.Value);
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

            DesbloqueoModel obs = ObtenerDesbloqueoPorId(id.Value);

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
            DesbloqueoModel obs = ObtenerDesbloqueoPorId(id.Value);

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
            bool resultado = EliminarDesbloqueo(id);

            if (resultado)
            {
                return RedirectToAction(nameof(Index)); // Redirige al índice si se eliminó correctamente
            }

            return NotFound(); // Si no se pudo eliminar, devuelve un error 404
        }

        // Método para eliminar un usuario de la base de datos
        private bool EliminarDesbloqueo(int id)
        {
            using (SqlConnection con = new(Configuration["ConnectionStrings:conexion"]))
            {
                using (SqlCommand cmd = new("ELIMINAR_DESBLOQUEOS", con))
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
