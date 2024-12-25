using BLL.Dtos.Category;
using BLL.Dtos.Utils;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminPanel.EndPoint.Pages.Category
{
    public class CreateModel : PageModel
    {
        private readonly ICategoryService categoryService;
        public CreateModel(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        [BindProperty]
        public CategoryDto category { get; set; }
        public ResultPageDto result { get; set; }
        public void OnGet()
        {
            result = new ResultPageDto(false, " ");
        }
        public async Task<IActionResult> OnPost()
        {
            if(category.Type == null)
            {
                result = new ResultPageDto(false, "لطفا مقدار دسته بندی را وارد کنید");
                return Page();
            }
            var insertResult = await categoryService.AddCategoryAsync(category);
            if (!insertResult.isSuccess)
            {
                result = new ResultPageDto(false, insertResult.Message);
                return Page();
            }
            result = new ResultPageDto(true, insertResult.Message);
            return Page();
        }
    }
}
