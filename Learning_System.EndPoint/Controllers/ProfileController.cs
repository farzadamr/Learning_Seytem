using BLL.Dtos.Utils;
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
        private readonly ICourseService courseService;
        public ProfileController(ICourseService courseService, IWalletService walletService, IUriComposer uriComposer, IPersonService personService, IStudentService studentService)
        {
            this.studentService = studentService;
            this.personService = personService;
            this.uriComposer = uriComposer;
            this.walletService = walletService;
            this.courseService = courseService;
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
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddCourseAsync(int CourseId, CancellationToken cancellationToken)
        {
            int userId = ClaimUtility.GetUserId(User);
            var student = await studentService.GetStudentByPersonId(userId);
            if (!student.isSuccess)
                return BadRequest();

            var wallet = await walletService.GetWalletAsync(student.Data.Id);
            var course = await courseService.GetCourseAsync(CourseId);

            if (!wallet.isSuccess)
                return BadRequest();
            var result = new ResultDto();
            if (wallet.Data.Credit < course.Data.Price)
            {
                result = new ResultDto
                {
                    isSuccess = false,
                    Message = "موجودی کیف پول کافی نمی‌باشد"
                };
                return Json(result);
            }

            var resultAdd = await studentService.AddCourseToStudentAsync(CourseId, student.Data.Id);
            if (resultAdd.isSuccess)
            {
                int count = (wallet.Data.Credit - course.Data.Price);
                var payRes = await walletService.ChargeWallet(student.Data.Id, count);
                if (payRes.isSuccess)
                {
                    return Json(resultAdd);
                }
                return Json(payRes);
            }
            return Json(resultAdd);
        }

    }
}
