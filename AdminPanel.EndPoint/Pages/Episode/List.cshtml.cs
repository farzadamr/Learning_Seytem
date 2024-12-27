using BLL.Dtos.Course;
using BLL.Dtos.Utils;
using BLL.ExternalApi;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AdminPanel.EndPoint.Pages.Episode
{
    public class ListModel : PageModel
    {
        private readonly ICourseService courseService;
        private readonly IUriComposer uriComposer;
        private readonly IFileUploadService uploadService;
        public ListModel(ICourseService courseService, IUriComposer uriComposer, IFileUploadService uploadService)
        {
            this.courseService = courseService;
            this.uriComposer = uriComposer;
            this.uploadService = uploadService;
        }
        [BindProperty]
        public EditEpisodeModel editEpisodeModel { get; set; }
        [BindProperty]
        public IFormFile? File { get; set; }
        public ResultPageDto result { get; set; }
        public string CourseName { get; set; }
        public List<EpisodeListDto> Episodes { get; set; }
        public void OnGet()
        {
            result = new ResultPageDto(false, "");
        }
        public async Task<IActionResult> OnPostEditEpisodeAsync()
        {
            if (!TryValidateModel(editEpisodeModel))
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(error=>error.ErrorMessage));
                result = new ResultPageDto(false, $"خطا در دریافت اطلاعات{string.Join(" , ", errors)}");
                return Page();
            }
            EditEpisodeDto episode = new EditEpisodeDto
            {
                Id = editEpisodeModel.Id,
                number = editEpisodeModel.number,
                Time = editEpisodeModel.Time
            };
            if (File != null)
            {
                var uploadResult = await uploadService.UploadAsync("Episodes", File);
                if (uploadResult.Status)
                {
                    episode.FilePath = uploadResult.FileNameAddress;
                }
                else
                {
                    result = new ResultPageDto(false, "خطا در آپلود فایل");
                    return Page();
                }
            }
            else
            {
                episode.FilePath = "";
            }
            var editResult = await courseService.EditEpisodeAsync(episode);
            if (editResult.isSuccess)
            {
                result = new ResultPageDto(true, editResult.Message);
                return Page();
            }
            result = new ResultPageDto(false, editResult.Message);
            return Page();          
        }
        public async Task<IActionResult> OnPostDeleteEpisodeAsync(int episodeId)
        {
            if(episodeId == 0)
            {
                result = new ResultPageDto(false, "خطا در دریافت اطلاعات");
                return Page();
            }
            var deleteResult = await courseService.DeleteEpisodeAsync(episodeId);
            if (deleteResult.isSuccess)
            {
                result = new ResultPageDto(true, deleteResult.Message);
                return Page();
            }
            result = new ResultPageDto(false, deleteResult.Message);
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
                var episodeFind = await courseService.GetEpisodeListAsync(courseId);
                if (episodeFind.isSuccess)
                {
                    foreach (var ep in episodeFind.Data)
                    {
                        ep.FilePath = uriComposer.Compose(ep.FilePath);
                    }
                    Episodes = episodeFind.Data;
                    result = new ResultPageDto(true, episodeFind.Message);
                    return Page();
                }
                result = new ResultPageDto(false, episodeFind.Message);
                return Page();
            }
            result = new ResultPageDto(false, findCourseResult.Message);
            return Page();
        }
        public class EditEpisodeModel
        {
            public int Id { get; set; }
            public int number { get; set; }
            public int Time { get; set; }
        }
    }
}
