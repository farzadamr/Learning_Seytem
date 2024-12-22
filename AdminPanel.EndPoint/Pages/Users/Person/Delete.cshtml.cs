using BLL.Dtos.Users;
using BLL.Dtos.Utils;
using BLL.ExternalApi;
using BLL.Interfaces;
using DAL.Repositories.Person;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminPanel.EndPoint.Pages.Users.Person
{
    public class DeleteModel : PageModel
    {
        private readonly IPersonService personService;
        private readonly IUriComposer uriComposer;
        public DeleteModel(IPersonService personService,IUriComposer uriComposer)
        {
            this.personService = personService;
            this.uriComposer = uriComposer;
        }
        [BindProperty]
        public string EmailAddress { get; set; }
        [BindProperty]
        public int PersonId { get; set; }
        public PersonDto Person { get; set; }
        public ResultPageDto result { get; set; }


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
            return Page();
        }
        public async Task<IActionResult> OnPostDeletePerson()
        {
            if(PersonId == null)
            {
                result = new ResultPageDto(false, "خطا در دریافت اطلاعات کاربر");
                return Page();
            }

        }
    }
}
