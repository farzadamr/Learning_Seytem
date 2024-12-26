using BLL.Dtos.Course;
using BLL.Dtos.Utils;
using BLL.ExternalApi;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace AdminPanel.EndPoint.Pages.Course
{
    public class EditModel : PageModel
    {
        private readonly ICourseService courseService;
        private readonly IFileUploadService uploadService;
        private readonly IUriComposer uriComposer;
        public EditModel(ICourseService courseService, IFileUploadService uploadService, IUriComposer uriComposer)
        {
            this.courseService = courseService;
            this.uploadService = uploadService;
            this.uriComposer = uriComposer;
        }

        [BindProperty]
        public EditCourseModel Course { get; set; }
        public ResultPageDto result { get; set; }
        [BindProperty]
        public IFormFile ThumbnailEdit { get; set; }
        [BindProperty]
        public IFormFile DemoVideoEdit { get; set; }
        public void OnGet()
        {
            result = new ResultPageDto(false, "");
        }
        public async Task<IActionResult> OnPostEditCourseAsync()
        {
            //if (!TryValidateModel(Course))
            //{
            //    result = new ResultPageDto(false, "خطا در دریافت مقادیر");
            //    return Page();
            //}
            CourseDto editCourseModel = new CourseDto
            {
                Id = Course.Id,
                Title = Course.Title,
                Description = Course.Description,
                Status = Course.Status,
                Capacity = Course.Capacity,
                CreationDate = Course.CreationDate,
                UpdateTime = Course.UpdateTime,
                Price = Course.Price,
                Time = Course.Time
            };
            if(ThumbnailEdit != null)
            {
                var uploadResult = await uploadService.UploadAsync("CourseImages", ThumbnailEdit);
                if (uploadResult.Status)
                    editCourseModel.ThumbnailPath = uploadResult.FileNameAddress;
            }

            if(DemoVideoEdit != null)
            {
                var uploadResult = await uploadService.UploadAsync("CourseImages", DemoVideoEdit);
                if (uploadResult.Status)
                    editCourseModel.DemoVideoPath = uploadResult.FileNameAddress;
            }
            var editResult = await courseService.EditCourseAsync(editCourseModel);
            if (editResult.isSuccess)
            {
                result = new ResultPageDto(true, editResult.Message);
                return Page();
            }
            result = new ResultPageDto(false, editResult.Message);
            return Page();


        }
        public async Task<IActionResult> OnPostFindCourseAsync(int CourseId)
        {
            if ( CourseId == 0)
            {
                result = new ResultPageDto(false, "خطا در دریافت اطلاعات");
                return Page();
            }
            var courseResult = await courseService.GetCourseAsync(CourseId);
            if (courseResult.isSuccess)
            {

                Course = new EditCourseModel
                {
                    Id = courseResult.Data.Id,
                    Title = courseResult.Data.Title,
                    Capacity = courseResult.Data.Capacity,
                    DemoVideoPath = string.IsNullOrWhiteSpace(courseResult.Data.DemoVideoPath) ? null : uriComposer.Compose(courseResult.Data.DemoVideoPath),
                    Description = courseResult.Data.Description,
                    CreationDate = courseResult.Data.CreationDate,
                    Price = courseResult.Data.Price,
                    Time = courseResult.Data.Time,
                    Status = courseResult.Data.Status,
                    ThumbnailPath = uriComposer.Compose(courseResult.Data.ThumbnailPath),
                    UpdateTime = courseResult.Data.UpdateTime
                };
                result = new ResultPageDto(true, courseResult.Message);
                return Page();
            }
            result = new ResultPageDto(false, courseResult.Message);
            return Page();
        }
        public class EditCourseModel
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime CreationDate { get; set; }
            public DateTime UpdateTime { get; set; }
            public int Capacity { get; set; }
            public string? ThumbnailPath { get; set; } 
            public string? DemoVideoPath { get; set; } 
            public int Time { get; set; }
            public string Status { get; set; }
            public int Price { get; set; }
        }
    }
}
