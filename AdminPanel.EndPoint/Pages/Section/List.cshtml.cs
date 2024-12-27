using BLL.Dtos.Course;
using BLL.Dtos.Utils;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminPanel.EndPoint.Pages.Section
{
    public class ListModel : PageModel
    {
        private readonly ICourseService courseService;
        public ListModel(ICourseService courseService)
        {
            this.courseService = courseService;
        }
        public string CourseName { get; set; }
        public ResultPageDto result { get; set; }
        public List<SectionDto> Sections { get; set; }
        public void OnGet()
        {
            result = new ResultPageDto(false, "");
        }



        public async Task<IActionResult> OnPostFindCourseAsync(int courseId)
        {
            if (courseId == 0)
            {
                result = new ResultPageDto(false, "خطا در دریافت اطلاعات");
                return Page();
            }

            var findCourseResult = await courseService.GetCourseAsync(courseId);
            if (findCourseResult.isSuccess)
            {
                CourseName = findCourseResult.Data.Title;
                var sectionFind = await courseService.GetSectionListAsync(courseId);
                if (sectionFind.isSuccess)
                {
                    Sections = sectionFind.Data;
                    result = new ResultPageDto(true, sectionFind.Message);
                    return Page();
                }
                result = new ResultPageDto(false, sectionFind.Message);
                return Page();
            }
            result = new ResultPageDto(false, findCourseResult.Message);
            return Page();
        }
    }
}
