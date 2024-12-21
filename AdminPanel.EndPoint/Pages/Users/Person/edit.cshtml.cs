using BLL.Dtos.Users;
using BLL.Dtos.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.PortableExecutable;

namespace AdminPanel.EndPoint.Pages.Users.Person
{
    public class editModel : PageModel
    {
        [BindProperty]
        public string EmailAddress { get; set; }
        public PersonDto Person { get; set; }
        [BindProperty]
        public PersonDto PersonModel { get; set; }
        public ResultDto result { get; set; }

        public void OnGet()
        {
            
        }
        public async Task<IActionResult> OnPostFindPerson()
        {
            if (string.IsNullOrWhiteSpace(EmailAddress)) return Page();
            // find person
            Person = new PersonDto()
            {
                FirstName = "farzad",
                LastName = "amiri"
            };
            return Page();
        }
        public async Task<IActionResult> OnPostEditPerson()
        {
           
            return Page();
        }
    }
}
