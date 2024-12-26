using BLL.Dtos.Course;
using BLL.Dtos.Utils;
using BLL.ExternalApi;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminPanel.EndPoint.Pages.Course
{
    public class ListModel : PageModel
    {
        private readonly ICourseService courseService;
        private readonly IUriComposer uriComposer;
        public ListModel(ICourseService courseService,IUriComposer uriComposer)
        {
            this.courseService = courseService;
            this.uriComposer = uriComposer;
        }

        public List<CourseListDto> Courses { get; set; }
        public ResultPageDto result { get; set; }
        public async Task OnGetAsync()
        {
            var getCourses = await courseService.GetCourseListAsync();
            if (getCourses.isSuccess)
            {
                Courses = getCourses.Data;
                foreach(var course in Courses)
                {
                    course.ThumbnailPath = uriComposer.Compose(course.ThumbnailPath);
                    course.DemoVideoPath = string.IsNullOrWhiteSpace(course.DemoVideoPath) ? null : uriComposer.Compose(course.DemoVideoPath);
                }
                result = new ResultPageDto(true, "");
            }
            else
            {
                Courses = new List<CourseListDto>();
                result = new ResultPageDto(false, getCourses.Message);
            }
        }
        public async Task<IActionResult> OnPostAsync(int courseId)
        {
            if (courseId == 0)
            {
                result = new ResultPageDto(false, "خطا در دریافت اطلاعات");
                return Page();
            }
            var deleteResult = await courseService.DeleteCourseAsync(courseId);
            if (deleteResult.isSuccess)
            {
                result = new ResultPageDto(true, deleteResult.Message);
                return Redirect("/course/list");
            }
            result = new ResultPageDto(false, deleteResult.Message);
            return Page();

        }
    }
}
