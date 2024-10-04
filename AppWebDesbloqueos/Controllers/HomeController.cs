using AppWebDesbloqueos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;

namespace AppWebDesbloqueos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly DashboardData _dashboardData;

        public HomeController(IConfiguration configuration)
        {
            _dashboardData = new DashboardData(configuration);
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        

      

        public IActionResult Index()
        {
            var username = User.Identity.Name; // Captura el nombre de usuario de Active Directory

            if (username!=null)
            {
                // Usa el nombre de usuario según sea necesario
                ViewBag.Username = username.Substring(8).ToUpper();
            }

            else
            {
                ViewBag.Username = "BANTRAB/Pruebas_Bantrab";
            }

            // Obtén los datos para los gráficos y el total de registros
            int totalRegistros = 0;
            int totalPendientes = 0;
            int totalAtendidosHoy = 0;

            using (SqlConnection connection = new SqlConnection(Configuration["ConnectionStrings:conexion"]))
            {
                string query = "SELECT COUNT(*) FROM Desbloqueos";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                totalRegistros = (int)command.ExecuteScalar();
            }

            using (SqlConnection connection = new SqlConnection(Configuration["ConnectionStrings:conexion"]))
            {
                string query = "select COUNT('x')\r\nfrom DESBLOQUEOS\r\nWHERE ESTADO='PENDIENTE'";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                totalPendientes = (int)command.ExecuteScalar();
            }

            using (SqlConnection connection = new SqlConnection(Configuration["ConnectionStrings:conexion"]))
            {
                string query = "select COUNT('x')\r\nfrom DESBLOQUEOS\r\nWHERE CONVERT(VARCHAR,FECHA_CORREO,103) = CONVERT(VARCHAR,GETDATE(),103)";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                totalAtendidosHoy = (int)command.ExecuteScalar();
            }

            // Pasar datos a la vista
            ViewBag.TotalRegistros = totalRegistros;
            ViewBag.TotalPendientes = totalPendientes;
            ViewBag.TotalAtendidosHoy = totalAtendidosHoy;

            // Aquí irían los datos de las gráficas
            // ViewBag.GraphData1 = ...;
            // ViewBag.GraphData2 = ...;

            

            // Obtener los datos de los estados para la gráfica
            var viewModel = new DashboardViewModel
            {
                ListaCounts = _dashboardData.GetListaCounts() ?? new List<KeyValuePair<string, int>>(),
                MesesCounts = _dashboardData.GetMesesCounts() ?? new List<KeyValuePair<string, int>>(),
                DiasCounts = _dashboardData.GetDiasCounts() ?? new List<KeyValuePair<string, int>>(),
                CnCounts = _dashboardData.GetCnCounts() ?? new List<KeyValuePair<string, int>>(),
                PendientesCounts = _dashboardData.GetPendientesCounts() ?? new List<KeyValuePair<string, int>>(),
                AtendidosHoyCounts = _dashboardData.GetAtendidosHoyCounts() ?? new List<KeyValuePair<string, int>>()
            };

            // Pasar los datos a la vista principal
            return View(viewModel);


            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
