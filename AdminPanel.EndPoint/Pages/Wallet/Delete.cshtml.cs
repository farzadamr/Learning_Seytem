using BLL.Dtos.Utils;
using BLL.Dtos.Wallet;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminPanel.EndPoint.Pages.Wallet
{
    public class DeleteModel : PageModel
    {
        private readonly IWalletService walletService;
        private readonly IStudentService studentService;
        private readonly IPersonService personService;
        public DeleteModel(IWalletService walletService, IStudentService studentService, IPersonService personService)
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
        public async Task<IActionResult> OnPostDeleteWallet()
        {
            if (Wallet.StudentId == 0)
            {
                result = new ResultPageDto(false, "مقدار را وارد کنید");
                return Page();
            }
            var deleteWalletResult = await walletService.DeleteWalletAsync(Wallet.StudentId);
            if (!deleteWalletResult.isSuccess)
            {
                result = new ResultPageDto(false, deleteWalletResult.Message);
                return Page();
            }
            result = new ResultPageDto(true, deleteWalletResult.Message);
            return Page();

        }
        public async Task<IActionResult> OnPostFindStudent()
        {
            if (StudentId == 0 || StudentId == null)
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
                    var credit = await walletService.GetWalletAsync(StudentId);
                    if (!credit.isSuccess)
                    {
                        result = new ResultPageDto(false, credit.Message);
                        return Page();
                    }
                    Wallet.Credit = credit.Data.Credit;
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
