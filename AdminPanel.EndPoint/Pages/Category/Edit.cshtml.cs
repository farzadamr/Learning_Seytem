using BLL.Dtos.Category;
using BLL.Dtos.Utils;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminPanel.EndPoint.Pages.Category
{
    public class EditModel : PageModel
    {
        private readonly ICategoryService categoryService;
        public EditModel(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        [BindProperty]
        public CategoryDto category { get; set; }
        public ResultPageDto result { get; set; }
        public void OnGet()
        {
            result = new ResultPageDto(false, "");
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (category.Id == 0 || category.Type == null)
            {
                result = new ResultPageDto(false, "خطا در  دریافت اطلاعات");
                return Page();
            }
            var findCategoryResult = await categoryService.GetCategoryAsync(category.Id);
            if (!findCategoryResult.isSuccess) 
            {
                result = new ResultPageDto(false, findCategoryResult.Message);
                return Page();
            }
            var editCategoryResult = await categoryService.EditCategoryAsync(category);
            if (!editCategoryResult.isSuccess)
            {
                result = new ResultPageDto(false, editCategoryResult.Message);
                return Page();
            }
            result = new ResultPageDto(true, $"ویرایش دسته بندی {findCategoryResult.Data.Type} با موفقیت انجام شد");
            return Page();
        }
    }
}
