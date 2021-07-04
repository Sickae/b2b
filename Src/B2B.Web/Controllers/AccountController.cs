using System.Threading.Tasks;
using AutoMapper;
using B2B.Logic.Identity;
using B2B.Web.Models;
using FluentNHibernate.Conventions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace B2B.Web.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IdentityUserManager _identityUserManager;
        private readonly SignInManager _signInManager;

        public AccountController(IMediator mediator,
            IMapper mapper,
            IdentityUserManager identityUserManager,
            SignInManager signInManager)
            : base(mediator, mapper)
        {
            _identityUserManager = identityUserManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var appUser = Mapper.Map<AppIdentityUser>(model);
            var result = await _identityUserManager.CreateAsync(appUser, model.Password);

            if (!result.Succeeded)
                return View(model);

            await _signInManager.SignInAsync(appUser, true);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl ?? Url.Content("~/")
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _identityUserManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                ModelState.AddModelError("", "Username or password is incorrect");
                return View(model);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (result?.Succeeded != true) return View(model);

            await _signInManager.SignInAsync(user, true);
            return LocalRedirect(model.ReturnUrl);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
