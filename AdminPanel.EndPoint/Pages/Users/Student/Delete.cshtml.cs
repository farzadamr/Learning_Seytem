using BLL.Dtos.Users;
using BLL.Dtos.Utils;
using BLL.Interfaces;
using DAL.Repositories.Person;
using DAL.Repositories.Student;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminPanel.EndPoint.Pages.Users.Student
{
    public class DeleteModel : PageModel
    {

        private readonly IPersonService personService;
        private readonly IStudentService studentService;
        public DeleteModel(IPersonService personService, IStudentService studentService)
        {
            this.personService = personService;
            this.studentService = studentService;
        }

        [BindProperty]
        public StudentDto Student { get; set; }
        public ResultPageDto result { get; set; }
        public string FullName { get; set; }

        public void OnGet()
        {
            result = new ResultPageDto(false, "");
            FullName = "";
        }

        public async Task<IActionResult> OnPostDeleteStudent()
        {
            if(Student.Id == null)
            {
                result = new ResultPageDto(false, "خطا در دریافت اطلاعات");
                return Page();
            }
            var deleteResult = await studentService.DeleteStudentById(Student.Id);
            if (!deleteResult.isSuccess)
            {
                result = new ResultPageDto(false, deleteResult.Message);
                return Page();
            }
            result = new ResultPageDto(true, deleteResult.Message);
            return Page();
        }

        public async Task<IActionResult> OnPostFindStudent()
        {
            if (Student.Id == 0)
            {
                result = new ResultPageDto(false, "شماره دانشجو را وارد نمایید");
                return Page();
            }
            var studentFindResult = await studentService.GetStudentById(Student.Id);
            if (!studentFindResult.isSuccess)
            {
                result = new ResultPageDto(false, studentFindResult.Message);
                return Page();
            }
            Student = new StudentDto
            {
                Id = studentFindResult.Data.Id,
                Major = studentFindResult.Data.Major,
                ActivityArea = studentFindResult.Data.ActivityArea,
                LinkedId = studentFindResult.Data.LinkedId
            };
            result = new ResultPageDto(true, studentFindResult.Message);
            var person = await personService.GetPersonById(studentFindResult.Data.PersonId);
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
