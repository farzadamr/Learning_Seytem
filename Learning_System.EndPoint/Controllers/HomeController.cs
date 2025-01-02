using BLL.Interfaces;
using Learning_System.EndPoint.Models;
using Learning_System.EndPoint.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Learning_System.EndPoint.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWalletService _walletService;
        private readonly IStudentService _studentService;
        public HomeController(ILogger<HomeController> logger, IStudentService studentService, IWalletService walletService)
        {
            _logger = logger;
            _walletService = walletService;
            _studentService = studentService;
        }

        public async Task<IActionResult> Index()
        {

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
