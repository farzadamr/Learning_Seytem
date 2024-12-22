using BLL.Dtos.Users;
using BLL.Dtos.Utils;
using BLL.ExternalApi;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.PortableExecutable;

namespace AdminPanel.EndPoint.Pages.Users.Person
{
    public class editModel : PageModel
    {
        private readonly IPersonService personService;
        private readonly IFileUploadService uploadService;
        private readonly IUriComposer uriComposer;
        public editModel(IPersonService personService, IFileUploadService uploadService,IUriComposer uriComposer)
        {
            this.personService = personService;
            this.uploadService = uploadService;
            this.uriComposer = uriComposer;
        }


        [BindProperty]
        public string EmailAddress { get; set; }
        public PersonDto Person { get; set; }
        [BindProperty]
        public PersonDto PersonModel { get; set; }
        public ResultPageDto result { get; set; }
        [BindProperty]
        public IFormFile Image { get; set; }
        [BindProperty]
        public string ImageAddress { get; set; }

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
                Id = findResult.Data.Id,
                FirstName = findResult.Data.FirstName,
                LastName = findResult.Data.LastName,
                PhoneNumber = findResult.Data.PhoneNumber,
                Email = findResult.Data.Email,
                Password = findResult.Data.Password,
                AvatarPath = uriComposer.Compose(findResult.Data.AvatarPath)
            };
            ImageAddress = findResult.Data.AvatarPath;
            return Page();
        }
        public async Task<IActionResult> OnPostEditPerson()
        {
            if (Image != null)
            {
                var uploadImage = await uploadService.UploadAsync("Persons", Image);
                if (uploadImage.Status)
                {
                    PersonModel.AvatarPath = uploadImage.FileNameAddress;
                }
                result = new ResultPageDto(false, "خطا در دریافت عکس");
                return Page();

            }
            PersonModel.AvatarPath = ImageAddress;
            var editResult = await personService.EditPerson(PersonModel);
            if (editResult.isSuccess)
            {
                result = new ResultPageDto(true, editResult.Message);
                return Page();
            }
            result = new ResultPageDto(false, editResult.Message);
            return Page();

        }
    }
}
