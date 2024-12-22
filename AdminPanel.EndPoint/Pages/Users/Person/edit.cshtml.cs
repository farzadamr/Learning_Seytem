using BLL.Dtos.Users;
using BLL.Dtos.Utils;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.PortableExecutable;

namespace AdminPanel.EndPoint.Pages.Users.Person
{
    public class editModel : PageModel
    {
        private readonly IPersonService personService;
        public editModel(IPersonService personService)
        {
            this.personService = personService;
        }


        [BindProperty]
        public string EmailAddress { get; set; }
        public PersonDto Person { get; set; }
        [BindProperty]
        public PersonDto PersonModel { get; set; }
        public ResultPageDto result { get; set; }
        public IFormFile Image { get; set; }

        public void OnGet()
        {
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
            Person = new PersonDto()
            {
                FirstName = findResult.Data.FirstName,
                LastName = findResult.Data.LastName,
                PhoneNumber = findResult.Data.PhoneNumber,
                Email = findResult.Data.Email,
                Password = findResult.Data.Password,
                AvatarPath = findResult.Data.AvatarPath
            };


            return Page();
        }
        public async Task<IActionResult> OnPostEditPerson()
        {
           
            return Page();
        }
    }
}
