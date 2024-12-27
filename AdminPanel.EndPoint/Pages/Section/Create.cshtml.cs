using BLL.Dtos.Course;
using BLL.Dtos.Utils;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminPanel.EndPoint.Pages.Section
{
    public class CreateModel : PageModel
    {
        private readonly ICourseService courseService;
        public CreateModel(ICourseService courseService)
        {
            this.courseService = courseService;
        }
        public string CourseName { get; set; }
        public int CourseId { get; set; }
        public ResultPageDto result { get; set; }
        [BindProperty]
        public AddSectionDto AddSection { get; set; }
        public void OnGet()
        {
            result = new ResultPageDto(false, "");
        }
        public async Task<IActionResult> OnPostAddSectionAsync()
        {
            if (!TryValidateModel(AddSection))
            {
                result = new ResultPageDto(false, "خطا در دریافت اطلاعات");
                return Page();
            }
            var addSectionResult = await courseService.AddSectionAsync(AddSection);
            if (addSectionResult.isSuccess)
            {
                result = new ResultPageDto(true, addSectionResult.Message);
                return Page();
            }
            result = new ResultPageDto(false, addSectionResult.Message);
            return Page();

        }
        public async Task<IActionResult> OnPostFindCourseAsync(int courseId)
        {
            if(courseId ==0)
            {
                result = new ResultPageDto(false, "خطا در دریافت اطلاعات");
                return Page();
            }

            var findCourseResult = await courseService.GetCourseAsync(courseId);
            if (findCourseResult.isSuccess)
            {
                CourseName = findCourseResult.Data.Title;
                CourseId = findCourseResult.Data.Id;
                result = new ResultPageDto(true, findCourseResult.Message);
                return Page();
            }
            result = new ResultPageDto(false, findCourseResult.Message);
            return Page();
        }

    }
}
