using BLL.Dtos.Course;
using BLL.Dtos.Utils;
using BLL.ExternalApi;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdminPanel.EndPoint.Pages.Episode
{
    public class CreateModel : PageModel
    {
        private readonly ICourseService courseService;
        private readonly IFileUploadService uploadService;
        public CreateModel(ICourseService courseService, IFileUploadService uploadService)
        {
            this.courseService = courseService;
            this.uploadService = uploadService;
        }
        [BindProperty]
        public AddEpisodeDto addEpisodeModel { get; set; }
        [BindProperty]
        public IFormFile File { get; set; }
        public ResultPageDto result { get; set; }
        public SelectList Sections { get; set; }
        public string CourseName { get; set; }
        public int CourseId { get; set; }
        public void OnGet()
        {
            result = new ResultPageDto(false, "");
        }
        public async Task<IActionResult> OnPostAddEpisodeAsync()
        {
            if (!ModelState.IsValid)
            {
                result = new ResultPageDto(false, "خطا در دریافت اطلاعات");
                return Page();
            }
            EpisodeDto episode = new EpisodeDto
            {
                number = addEpisodeModel.number,
                Time = addEpisodeModel.Time,
                sectionId = addEpisodeModel.sectionId,
                CourseId = addEpisodeModel.CourseId,
                Visit = 0,
            };
            var uploadResult = await uploadService.UploadAsync("Episodes", File);
            if (uploadResult.Status)
                episode.FilePath = uploadResult.FileNameAddress;
            else
            {
                result = new ResultPageDto(false, "خطا در دریافت فایل ویدیویی");
                return Page();
            }

            var addEpisodeResult = await courseService.AddEpisodeAsync(episode);
            if (addEpisodeResult.isSuccess)
            {
                result = new ResultPageDto(true, addEpisodeResult.Message);
                return Page();
            }
            result = new ResultPageDto(false, addEpisodeResult.Message);
            return Page();
        }
        public async Task<IActionResult> OnPostFindCourseAsync(int courseId)
        {
            if (courseId == 0)
            {
                result = new ResultPageDto(false, "خطا در دریافت اطلاعات");
                return Page();
            }
            var courseFind = await courseService.GetCourseAsync(courseId);
            if (courseFind.isSuccess)
            {
                CourseId = courseId;
                CourseName = courseFind.Data.Title;
                var sectionFind = await courseService.GetSectionListAsync(courseId);
                if (sectionFind.isSuccess)
                {
                    var sectionList = sectionFind.Data.Select(section => new sectionListDto
                    {
                        Id = section.Id,
                        Title = section.Title
                    });
                    Sections = new SelectList(sectionList, "Id", "Title");
                    result = new ResultPageDto(true, "");
                    return Page();
                }
                result = new ResultPageDto(false, sectionFind.Message);
                return Page();
            }
            result = new ResultPageDto(false, courseFind.Message);
            return Page();
        }
        public class sectionListDto
        {
            public int Id { get; set; }
            public string Title { get; set; }
        }
        public class AddEpisodeDto
        {
            public int number { get; set; }
            public int sectionId { get; set; }
            public int CourseId { get; set; }
            public int Time { get; set; }
            public int Visit { get; set; }
        }
    }
}
