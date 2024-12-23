using BLL.Dtos.Users;
using BLL.Dtos.Utils;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminPanel.EndPoint.Pages.Users.Teacher
{
    public class CreateModel : PageModel
    {
        private readonly IPersonService personService;
        private readonly ITeacherService teacherService;
        public CreateModel(IPersonService personService,ITeacherService teacherService)
        {
            this.personService = personService;
            this.teacherService = teacherService;
        }
        public ResultPageDto result { get; set; }
        [BindProperty]
        public int PersonId { get; set; }
        public string FullName { get; set; }
        [BindProperty]
        public TeacherDto teacherModel { get; set; }
        [BindProperty]
        public string EmailAddress { get; set; }

        public void OnGet()
        {
            FullName = "";
            result = new ResultPageDto(false, " ");
        }
        public async Task<IActionResult> OnPostAddTeacher()
        {
            if(PersonId == null || string.IsNullOrWhiteSpace(teacherModel.Resume))
            {
                result = new ResultPageDto(false, "خطا در دریافت اطلاعات");
                return Page();
            }
            teacherModel.PersonID = PersonId;
            var insertionResult = await teacherService.AddTeacher(teacherModel);
            if (!insertionResult.isSuccess)
            {
                result = new ResultPageDto(false, insertionResult.Message);
                return Page();
            }
            result = new ResultPageDto(true, insertionResult.Message);
            return Page();
        }
        public async Task<IActionResult> OnPostFindPerson()
        {
            if (string.IsNullOrWhiteSpace(EmailAddress))
            {
                result = new ResultPageDto(false, "مقدار ایمیل را وارد کنید");
                return Page();
            }
            // find person
            var findResult = await personService.GetPersonByEmail(EmailAddress);
            if (!findResult.isSuccess)
            {
                result = new ResultPageDto(false, findResult.Message);
                return Page();
            }
            result = new ResultPageDto(true, findResult.Message);
            PersonId = findResult.Data.Id;
            FullName = findResult.Data.FirstName + " " + findResult.Data.LastName;
            return Page();
        }
    }
}
