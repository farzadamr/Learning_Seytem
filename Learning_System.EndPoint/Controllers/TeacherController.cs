using BLL.Dtos.Course;
using BLL.ExternalApi;
using BLL.Interfaces;
using Learning_System.EndPoint.Models;
using Microsoft.AspNetCore.Mvc;

namespace Learning_System.EndPoint.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ITeacherService teacherService;
        private readonly IPersonService personService;
        private readonly IProductService productService;
        private readonly ISearchService searchService;
        private readonly IUriComposer uriComposer;
        public TeacherController(IUriComposer uriComposer, ISearchService searchService, IProductService productService, IPersonService personService, ITeacherService teacherService)
        {
            this.uriComposer = uriComposer;
            this.searchService = searchService;
            this.personService = personService;
            this.teacherService = teacherService;
            this.productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var data = await searchService.GetTeacherByEmailSearch("");
            foreach(var teacher in data.Data)
            {
                teacher.AvatarPath = uriComposer.Compose(teacher.AvatarPath);
            }
            return View(data.Data);
        }
        public async Task<IActionResult> Detail(int Id)
        {
            var teacher = await teacherService.GetTeacherById(Id);
            var personTeacher = await personService.GetPersonById(teacher.Data.PersonID);
            var Courses = await productService.GetCoursesByTeacherIdAsync(Id);
            foreach(var c in Courses)
            {
                c.ThumbnailPath = uriComposer.Compose(c.ThumbnailPath);
            }
            var viewModel = new TeacherViewModel
            {
                Id = Id,
                FullName = personTeacher.Data.FirstName + " " + personTeacher.Data.LastName,
                Avatar = uriComposer.Compose(personTeacher.Data.AvatarPath),
                Resume = teacher.Data.Resume,
                Courses = Courses
            };
            return View(viewModel);
        }
    }
}
