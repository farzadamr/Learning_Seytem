using AutoMapper;
using BLL.Dtos.Users;
using BLL.Interfaces;
using Learning_System.EndPoint.Models.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Learning_System.EndPoint.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;
        public AccountController(IUserManager _userManager,IMapper _mapper)
        {
            this._userManager = _userManager;
            this._mapper = _mapper;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            return Redirect("Home/Index");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var model = _mapper.Map<StudentDto>(viewModel);
            var result = await _userManager.RegisterStudentAsync(model);
            ViewBag.ResponseModel = result;

			return View();
        }
    }
}
