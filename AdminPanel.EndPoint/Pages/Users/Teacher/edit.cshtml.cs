using BLL.Dtos.Users;
using BLL.Dtos.Utils;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminPanel.EndPoint.Pages.Users.Teacher
{
    public class editModel : PageModel
    {
        private readonly ITeacherService teacherService;
        private readonly IPersonService personService;
        public editModel(ITeacherService teacherService, IPersonService personService)
        {
            this.teacherService = teacherService;
            this.personService = personService;
        }

        [BindProperty]
        public TeacherDto Teacher { get; set; }
        public string FullName { get; set; }
        public ResultPageDto result { get; set; }


        public void OnGet()
        {
            FullName = "";
            result = new ResultPageDto(false, "");
        }
        public async Task<IActionResult> OnPostEditTeacher()
        {
            if (!TryValidateModel(Teacher, nameof(Teacher))){
                result = new ResultPageDto(false, "خطا در دریافت اطلاعات");
                return Page();
            }
            var editResult = await teacherService.EditTeacher(Teacher);
            if (!editResult.isSuccess)
            {
                result = new ResultPageDto(false, editResult.Message);
                return Page();
            }
            result = new ResultPageDto(true, editResult.Message);
            return Page();
        }
        public async Task<IActionResult> OnPostFindTeacher()
        {
            if (Teacher.Id == 0)
            {
                result = new ResultPageDto(false, "شماره مدرس را وارد نمایید");
                return Page();
            }
            var teacherFindResult = await teacherService.GetTeacherById(Teacher.Id);
            if (!teacherFindResult.isSuccess)
            {
                result = new ResultPageDto(false, teacherFindResult.Message);
                return Page();
            }
            Teacher = new TeacherDto
            {
                Id = teacherFindResult.Data.Id,
                Resume = teacherFindResult.Data.Resume,
                PersonID = teacherFindResult.Data.PersonID
            };
            result = new ResultPageDto(true, teacherFindResult.Message);
            var person = await personService.GetPersonById(teacherFindResult.Data.PersonID);
            if (person == null)
            {
                result = new ResultPageDto(false, person.Message);
                return Page();
            }
            FullName = person.Data.FirstName + " " + person.Data.LastName;
            return Page();
        }
    }
}
