using BLL.Dtos.Utils;
using BLL.Dtos.Wallet;
using BLL.Interfaces;
using DAL.Entities.Wallet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminPanel.EndPoint.Pages.Wallet
{
    public class CreateModel : PageModel
    {
        private readonly IWalletService walletService;
        private readonly IStudentService studentService;
        private readonly IPersonService personService;
        public CreateModel(IWalletService walletService,IStudentService studentService,IPersonService personService)
        {
            this.studentService = studentService;
            this.walletService = walletService;
            this.personService = personService;

        }
        [BindProperty]
        public int StudentId { get; set; }
        [BindProperty]
        public WalletDto Wallet { get; set; }
        public string FullName { get; set; }
        public ResultPageDto result { get; set; }
        
        public void OnGet()
        {
            FullName = " ";
            result = new ResultPageDto(false, "");
        }
        public async Task<IActionResult> OnPostAddWallet()
        {
            if(Wallet.Credit == 0)
            {
                result = new ResultPageDto(false, "مقدار را وارد کنید"); 
                return Page();
            }
            var addWalletResult = await walletService.CreateWalletAsync(Wallet);
            if (!addWalletResult.isSuccess)
            {
                result = new ResultPageDto(false, addWalletResult.Message);
                return Page();
            }
            result = new ResultPageDto(true, addWalletResult.Message);
            return Page();

        }
        public async Task<IActionResult> OnPostFindStudent()
        {
            if(StudentId == 0 || StudentId == null)
            {
                result = new ResultPageDto(false, "خطا در دریافت اطلاعات");
                return Page();
            }
            var findStudent = await studentService.GetStudentById(StudentId);
            if (findStudent.isSuccess)
            {
                var findfullname = await personService.GetPersonById(findStudent.Data.PersonId);
                if (findfullname.isSuccess)
                {
                    FullName = findfullname.Data.FirstName + " " + findfullname.Data.LastName;
                    result = new ResultPageDto(true, findfullname.Message);
                    return Page();
                }
                result = new ResultPageDto(false, findfullname.Message);
                return Page();
            }
            result = new ResultPageDto(false, findStudent.Message);
            return Page();
            
        }
    }
}
