using BLL.ExternalApi;
using BLL.Interfaces;

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
				ViewBag.Response = courses;
				return View(courses.Data);
			}
			ViewBag.Response = courses;
			return View();

		}
	}
}
