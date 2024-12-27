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
        [BindProperty]
        public EditSectionDto sectionEditModel { get; set; }
        public void OnGet()
        {
            result = new ResultPageDto(false, "");
        }

        public async Task<IActionResult> OnPostDeleteSectionAsync(int sectionId)
        {
            if (sectionId == null)
            {
                result = new ResultPageDto(false, "خطا در دریافت اطلاعات");
                return Page();
            }
            var deleteResult = await courseService.DeleteSectionAsync(sectionId);
            if (deleteResult.isSuccess)
            {
                result = new ResultPageDto(true, deleteResult.Message);
                return Page();
            }
            result = new ResultPageDto(false, deleteResult.Message);
            return Page();
        }
        public async Task<IActionResult> OnPostEditSectionAsync()
        {
            if (!TryValidateModel(sectionEditModel))
            {
                result = new ResultPageDto(false, "خطا در دریافت اطلاعات");
                return Page();
            }
            var editSection = await courseService.EditSectionAsync(sectionEditModel);
            if (editSection.isSuccess)
            {
                result = new ResultPageDto(true, editSection.Message);
                return Page();
            }
            result = new ResultPageDto(false, editSection.Message);
            return Page();
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
