using BLL.Dtos.Users;
using BLL.Dtos.Utils;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminPanel.EndPoint.Pages.Users.Student
{
    public class EditModel : PageModel
    {
        private readonly IStudentService studentService;
        private readonly IPersonService personService;
        public EditModel(IStudentService studentService, IPersonService personService)
        {
            this.studentService = studentService;
            this.personService = personService;
        }

        [BindProperty]
        public StudentDto Student { get; set; }
        public string FullName { get; set; }
        public ResultPageDto result { get; set; }
        

        public void OnGet()
        {
            FullName = "";
            result = new ResultPageDto(false, "");
        }
        public async Task<IActionResult> OnPostFindStudent()
        {
            if(Student.Id == 0)
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
            if(person == null)
            {
                result = new ResultPageDto(false, person.Message);
                return Page();
            }
            FullName = person.Data.FirstName + " " + person.Data.LastName;
            return Page();
        }
    }
}
