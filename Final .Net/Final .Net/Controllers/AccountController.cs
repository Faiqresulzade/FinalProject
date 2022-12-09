using Final_.Net.Services.Abstract;
using Final_.Net.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;

namespace Final_.Net.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterVM model)
        {
            var isExist = await _accountService.Register(model);
            if (isExist != null) return RedirectToAction(nameof(Login));
            return View(model);
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginVM model)
        {
            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }
            else
            {
                return RedirectToAction("index", "home");
            }

            var isExist = await _accountService.Login(model);
            if(isExist!=null)return RedirectToAction("index","home");
            return View(model); //NotFound(); 
        }
    }
}
