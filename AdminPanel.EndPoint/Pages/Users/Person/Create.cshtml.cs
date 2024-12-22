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
        [BindProperty]
        public IFormFile Image { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) return Page();

            var uploadResult = await uploadService.UploadAsync("Persons", Image);
            string avatarPath = "";
            if (uploadResult.Status)
            {
                avatarPath = uploadResult.FileNameAddress;
            }
            else
            {
                //image error
                return Page();
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
            TempData["message"] = result.Message;
            return Page();
        }
    }
}
