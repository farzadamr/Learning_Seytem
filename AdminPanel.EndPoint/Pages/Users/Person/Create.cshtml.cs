using AdminPanel.EndPoint.ViewDto;
using BLL.ExternalApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.PortableExecutable;

namespace AdminPanel.EndPoint.Pages.Users.Person
{
    public class CreateModel : PageModel
    {
        private readonly IFileUploadService uploadService;
        public CreateModel(IFileUploadService uploadService)
        {
            this.uploadService = uploadService;
        }
        [BindProperty]
        public Add_PersonDto Person { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            if(Person.Avatar != null)
            {
                uploadService.UploadAsync("Persons", Person.Avatar);
            }
        }
    }
}
