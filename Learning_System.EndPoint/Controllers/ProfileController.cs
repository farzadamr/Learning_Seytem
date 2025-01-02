using BLL.ExternalApi;
using BLL.Interfaces;
using DAL.Repositories.Student;
using DAL.Repositories.Wallet;
using Learning_System.EndPoint.Models.Users;
using Learning_System.EndPoint.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Learning_System.EndPoint.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IStudentService studentService;
        private readonly IPersonService personService;
        private readonly IUriComposer uriComposer;
        private readonly IWalletService walletService;
        public ProfileController(IWalletService walletService, IUriComposer uriComposer, IPersonService personService, IStudentService studentService)
        {
            this.studentService = studentService;
            this.personService = personService;
            this.uriComposer = uriComposer;
            this.walletService = walletService;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            int userId = ClaimUtility.GetUserId(User);
            var student = await studentService.GetStudentByPersonId(userId);
            if (!student.isSuccess)
            {
                return BadRequest();
            }
            var personDetail = await personService.GetPersonById(userId);
            var courses = await studentService.GetStudentCourses(student.Data.Id);
            foreach(var c in courses)
            {
                c.ThumbnailPath = uriComposer.Compose(c.ThumbnailPath);
            }
			var wallet = await walletService.GetWalletAsync(student.Data.Id);
			
			var viewModel = new StudentViewModel
            {
                stId = student.Data.Id,
                FullName = personDetail.Data.FirstName + " " + personDetail.Data.LastName,
                Avatar = uriComposer.Compose(personDetail.Data.AvatarPath),
                Email = personDetail.Data.Email,
                LinkedId = student.Data.LinkedId,
                Credit = wallet.Data.Credit,
                PLP = courses
            };
            return View(viewModel);
        }
    }
}
