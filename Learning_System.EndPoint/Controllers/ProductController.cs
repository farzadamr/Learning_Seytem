using BLL.ExternalApi;
using BLL.Interfaces;
using Learning_System.EndPoint.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace Learning_System.EndPoint.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductService productService;
		private readonly ICategoryService categoryService;
		private readonly IUriComposer uriComposer;
		public ProductController(IProductService productService, IUriComposer uriComposer,ICategoryService categoryService)
		{
			this.productService = productService;
			this.uriComposer = uriComposer;
			this.categoryService = categoryService;
		}
		public async Task<IActionResult> Index(int categoryId = 0)
		{
			var categories = await categoryService.GetChildCategoryListAsync();
			ViewBag.Categories = categories.Data;
			ViewBag.CurrentCategory = categoryId;
			var courses = await productService.GetPLPAsync(categoryId);
			if (courses.isSuccess)
			{
				foreach (var course in courses.Data)
				{
					course.ThumbnailPath = uriComposer.Compose(course.ThumbnailPath);
				}
				return View(courses.Data);
			}
			return View();

		}
		public async Task<IActionResult> Detail(int CourseId)
		{
			var course = await productService.GetPDPAsync(CourseId);
			if (course.isSuccess)
			{
				course.Data.ThumbnailPath = uriComposer.Compose(course.Data.ThumbnailPath);
				course.Data.PersonAvatar = uriComposer.Compose(course.Data.PersonAvatar);

				var episodes = await productService.GetEpisodes(CourseId);
				if (episodes == null && episodes.Count == 0)
				{
					return View();
                }
                foreach (var ep in episodes)
                {
                    ep.FilePath = uriComposer.Compose(ep.FilePath);
                }
                var viewModel = new PDPViewModel
                {
                    pdpData = course.Data,
                    episodes = episodes
                };
                return View(viewModel);
			}
			return View();
		}
	}
}
