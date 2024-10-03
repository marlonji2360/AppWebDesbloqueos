using AppWebDesbloqueos.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppWebDesbloqueos.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DashboardData _dashboardData;

        public DashboardController(IConfiguration configuration)
        {
            _dashboardData = new DashboardData(configuration);
        }

        public IActionResult EstadoChart()
        {
            var estadoCounts = _dashboardData.GetListaCounts();
            return View(estadoCounts);
        }
    }
}
