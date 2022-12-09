using Final_.Net.Areas.Admin.Services.Abstract;
using Final_.Net.Areas.Admin.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;

namespace Final_.Net.Areas.Admin.Controllers
{
    [Area("admin")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
           _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginVM model)
        {
            var isExist = _accountService.LoginAsync(model);
            if(isExist!=null) return RedirectToAction("index", "dashboard");
            return View(model);
        }

    }
}
