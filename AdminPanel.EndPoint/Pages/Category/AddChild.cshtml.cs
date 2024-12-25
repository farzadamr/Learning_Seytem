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
            var findCategories = await categoryService.GetCategoryListAsync();
            categoryList = new SelectList(findCategories.Data, "Id", "Type");

        }
    }
}
