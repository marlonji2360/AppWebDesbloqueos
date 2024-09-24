using AppWebDesbloqueos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AppWebDesbloqueos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var username = User.Identity.Name; // Captura el nombre de usuario de Active Directory

            // Usa el nombre de usuario según sea necesario
            ViewBag.Username = username.Substring(8).ToUpper();

            return View();
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
