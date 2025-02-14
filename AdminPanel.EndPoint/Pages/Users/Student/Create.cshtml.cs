﻿using BLL.Dtos.Users;
using BLL.Dtos.Utils;
using BLL.ExternalApi;
using BLL.Interfaces;
using DAL.Repositories.Person;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminPanel.EndPoint.Pages.Users.Student
{
    public class CreateModel : PageModel
    {
        private readonly IStudentService studentService;
        private readonly IPersonService personService;
        public CreateModel(IStudentService studentService,IPersonService personService)
        {
            this.studentService = studentService;
            this.personService = personService;
        }
        public ResultPageDto result { get; set; }
        [BindProperty]
        public int PersonId { get; set; }
        public string FullName { get; set; }
        [BindProperty]
        public StudentDto StudentModel { get; set; }
        [BindProperty]
        public string EmailAddress { get; set; }

        public void OnGet()
        {
            FullName = "";
            result = new ResultPageDto(false, " ");
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
        public async Task<IActionResult> OnPostAddStudent()
        {
            StudentModel.PersonId = PersonId;
            StudentModel.Lockout = false;

            if (PersonId == null) return Page();
            var insertModel = await studentService.AddStudent(StudentModel);
            if (!insertModel.isSuccess)
            {
                result = new ResultPageDto(false, insertModel.Message);
                return Page();
            }
            result = new ResultPageDto(true, insertModel.Message);
            return Page();
        }
    }
}
