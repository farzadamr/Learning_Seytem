using AdminPanel.EndPoint.ViewDto;
using BLL.Dtos.Users;
using BLL.ExternalApi;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.PortableExecutable;

namespace AdminPanel.EndPoint.Pages.Users.Person
{
    public class CreateModel : PageModel
    {
        private readonly IFileUploadService uploadService;
        private readonly IPersonService personService;
        public CreateModel(IFileUploadService uploadService,IPersonService personService)
        {
            this.uploadService = uploadService;
            this.personService = personService;
        }
        [BindProperty]
        public Add_PersonDto Person { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            string avatarPath = "";
            if(Person.Avatar != null)
            {
                avatarPath = await uploadService.UploadAsync("Persons", Person.Avatar);
            }

            PersonDto person = new PersonDto()
            {
                FirstName = Person.FirstName,
                LastName = Person.LastName,
                Email = Person.Email,
                Password = Person.Password,
                PhoneNumber = Person.PhoneNumber,
                AvatarPath = avatarPath,
            };
            var result = await personService.AddPerson(person);
            TempData["isSuccess"] = result.isSuccess;
            TempData["message"] = result.Message;
            return Page();
        }
    }
}
