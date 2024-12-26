using BLL.Dtos.Course;
using BLL.Dtos.Utils;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdminPanel.EndPoint.Pages.Course
{
    public class CreateModel : PageModel
    {
        private readonly ISearchService searchService;
        private readonly ICategoryService categoryService;
        public CreateModel(ISearchService searchService,ICategoryService categoryService)
        {
            this.searchService = searchService;
            this.categoryService = categoryService;
        }
        [BindProperty]
        public AddCourseDto AddCourseModel { get; set; }

        public ResultPageDto result { get; set; }
        public SelectList Teachers { get; set; }
        public SelectList Categories { get; set; }
        public async Task OnGetAsync()
        {
            var teachersResult = await searchService.GetTeacherByEmailSearch("");
            if (teachersResult.isSuccess)
            {
                var teacherList = teachersResult.Data.Select(teacher => new teacherListViewModel
                {
                    Id = teacher.Id,
                    FullName = teacher.Firstname + " " + teacher.Lastname
                }).ToList();
                Teachers = new SelectList(teacherList, "Id", "FullName");
            }
            var categoriesResult = await categoryService.GetParentCategoryListAsync();
            List<categoryViewModel> parentCategoryList = new List<categoryViewModel>();
            if (categoriesResult.isSuccess)
            {
                parentCategoryList = categoriesResult.Data.Select(category => new categoryViewModel
                {
                    Id = category.Id,
                    Type = category.Type
                }).ToList();
            }
            var childCaregoryResult = await categoryService.GetChildCategoryListAsync();
            List<categoryViewModel> childCategoryList = new List<categoryViewModel>();
            if (childCaregoryResult.isSuccess)
            {
                childCategoryList = childCaregoryResult.Data.Select(child => new categoryViewModel
                {
                    Id = child.Id,
                    Type = child.ChildType + " - " + child.ParentType
                }).ToList();
            }
            var combinedCategoryList = parentCategoryList.Concat(childCategoryList).ToList();
            Categories = new SelectList(combinedCategoryList, "Id", "Type");
            result = new ResultPageDto(false, "");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!TryValidateModel(AddCourseModel))
            {
                result = new ResultPageDto(false, "اطلاعات را با دقت وارد کنید");
                return Page();
            }

            return Page();
        }


        public class teacherListViewModel
        {
            public int Id { get; set; }
            public string FullName { get; set; }
        }
        public class categoryViewModel
        {
            public int Id { get; set; }
            public string Type { get; set; }
        }
    }
}
