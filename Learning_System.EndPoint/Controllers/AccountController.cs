using AutoMapper;
using BLL.Dtos.Users;
using BLL.Interfaces;
using Learning_System.EndPoint.Models.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Learning_System.EndPoint.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;
        public AccountController(IUserManager _userManager, IMapper _mapper)
        {
            this._userManager = _userManager;
            this._mapper = _mapper;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            viewModel.Email = viewModel.Email.ToLower();
            var result = await _userManager.LoginPersonAsync(viewModel.Email, viewModel.Password);
            if (!result.isSuccess)
            {
                ViewBag.errorMessage = result.Message;
                return View(viewModel);
            }
            //Add Claim User
            if (result != null)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, result.Data.Id.ToString()),
                    new Claim(ClaimTypes.Name, result.Data.FullName),
                };
                foreach(var claim in result.Data.claims)
                {
                    claims.Add(new Claim(claim.Type, claim.Value));
                }
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.Now.AddDays(5),
                };
                await HttpContext.SignInAsync(principal, properties);
                return Redirect("/");
            }
            ViewBag.errorMessage = "خطا در ارتباط با سرور";
            return View(viewModel);
            
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            viewModel.Email = viewModel.Email.ToLower();
            var model = _mapper.Map<PersonDto>(viewModel);
            var result = await _userManager.RegisterPersonAsync(model);
            ViewBag.ResponseModel = result;

            return View();
        }
        public async Task<IActionResult> Logout()
        {
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
		}
    }
}
