using BLL.Dtos.Utils;
using BLL.Dtos.Wallet;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminPanel.EndPoint.Pages.Wallet
{
    public class ChargeModel : PageModel
    {
        private readonly IWalletService walletService;
        private readonly IStudentService studentService;
        private readonly IPersonService personService;
        public ChargeModel(IWalletService walletService, IStudentService studentService, IPersonService personService)
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
        public async Task<IActionResult> OnPostChargeWallet()
        {
            if (Wallet.Credit == 0)
            {
                result = new ResultPageDto(false, "مقدار را وارد کنید");
                return Page();
            }
            var findWallet = await walletService.GetWalletAsync(Wallet.StudentId);
            if (!findWallet.isSuccess)
            {
                result = new ResultPageDto(false, findWallet.Message);
                return Page();
            }
            Wallet.Credit += findWallet.Data.Credit;
            var chargeWalletResult = await walletService.ChargeWallet(Wallet.StudentId, Wallet.Credit);
            if (!chargeWalletResult.isSuccess)
            {
                result = new ResultPageDto(false, chargeWalletResult.Message);
                return Page();
            }
            result = new ResultPageDto(true, $"کیف پول با موفقیت شارژ شد موجودی فعلی : {Wallet.Credit.ToString("n0")}");
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
                        result = new ResultPageDto(false, credit.Message); return Page();
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
