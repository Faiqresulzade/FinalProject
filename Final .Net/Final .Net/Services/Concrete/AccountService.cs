using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Final_.Net.Services.Abstract;
using Final_.Net.ViewModels.Account;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Policy;

namespace Final_.Net.Services.Concrete
{
    public class AccountService:IAccountService
    {
        private readonly ModelStateDictionary _modelstate;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager,
                                                IActionContextAccessor actionContextAccessor)
        {
            _modelstate = actionContextAccessor.ActionContext.ModelState;
            _userManager = userManager;
            _signInManager = signInManager;
        }

      

        public async Task<AccountRegisterVM> Register(AccountRegisterVM model)
        {
            if (!_modelstate.IsValid) return null;
            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName,

            };
            var result =await _userManager.CreateAsync(user,model.PassWord);
            if (result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    _modelstate.AddModelError(string.Empty, error.Description);
                }
                return null;
            }
            return model;
        }

        public async Task<AccountLoginVM> Login(AccountLoginVM model)
        {
            if (!_modelstate.IsValid) return null;
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                _modelstate.AddModelError(string.Empty, "UserName or Password is InCorrect!!");
                return null;
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.PassWord, false, false);
            if (!result.Succeeded)
            {
                _modelstate.AddModelError(string.Empty, "UserName or Password is InCorrect!!");
                return null;
            }
            return model;
            //if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            //{
            //    return Redirect(model.ReturnUrl);
            //}
            //else
            //{
            //    return RedirectToAction("index", "home");

            //}
        }
    }
}
