using BLL.ExternalApi;
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
        private readonly IProductService productService;
        private readonly IUriComposer uriComposer;
        public HomeController(ILogger<HomeController> logger,IProductService productService, IUriComposer uriComposer)
        {
            _logger = logger;
            this.productService = productService;
            this.uriComposer = uriComposer;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await productService.GetPLPAsync();
            foreach(var course in courses.Data)
            {
                course.ThumbnailPath = uriComposer.Compose(course.ThumbnailPath); 
            }
            return View(courses.Data);
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
