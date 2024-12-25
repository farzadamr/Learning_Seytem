using BLL.Dtos.Category;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminPanel.EndPoint.Pages.Category
{
    public class IndexModel : PageModel
    {
        public List<CategoryDto> Categories { get; set; }
        public List<ParentChildDto> ChildCategories { get; set; }

        private readonly ICategoryService _categoryService;
        public IndexModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task OnGetAsync()
        {
            var findcategoryResult = await _categoryService.GetParentCategoryListAsync();
            if (findcategoryResult.isSuccess)
            {
                Categories = findcategoryResult.Data;
            }
            var findChildCategory = await _categoryService.GetChildCategoryListAsync();
            if (findChildCategory.isSuccess)
            {
                ChildCategories = findChildCategory.Data;
            }
        }

        public async Task<IActionResult> OnPostEditCategoryAsync(int editCategoryId, string editCategoryType)
        {
            var category = new CategoryDto { Id = editCategoryId, Type = editCategoryType };
            var editResult = await _categoryService.EditCategoryAsync(category);
            if (editResult.isSuccess)
            {

            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteCategoryAsync(int deleteCategoryId)
        {
            await _categoryService.DeleteCategoryAsync(deleteCategoryId);
            return RedirectToPage();
        }
    }
}

