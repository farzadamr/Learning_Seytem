using BLL.Dtos.Category;
using BLL.Dtos.Utils;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdminPanel.EndPoint.Pages.Category
{
    public class AddChildModel : PageModel
    {
        private readonly ICategoryService categoryService;
        public AddChildModel(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        [BindProperty]
        public CategoryDto category { get; set; }
        public SelectList categoryList { get; set; }
        public ResultPageDto result { get; set; }
        public async Task OnGetAsync()
        {
            result = new ResultPageDto(false, " ");
            var findCategories = await categoryService.GetParentCategoryListAsync();
            categoryList = new SelectList(findCategories.Data, "Id", "Type");

        }
        public async Task<IActionResult> OnPostAsync()
        {
            if(category.Type == null || category.ParentTypeId == null || category.ParentTypeId == 0)
            {
                result = new ResultPageDto(false, "خطا در دریافت اطلاعات");
                return Page();
            }
            var addCategoryResult = await categoryService.AddCategoryAsync(category);
            if (!addCategoryResult.isSuccess)
            {
                result = new ResultPageDto(false, addCategoryResult.Message);
                return Page();
            }
            result = new ResultPageDto(true, addCategoryResult.Message);
            return Page();
        }
    }
}
