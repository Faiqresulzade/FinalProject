using Core.Constants;
using Core.Entities;
using Final_.Net.Areas.Admin.Services.Abstract;
using Final_.Net.Areas.Admin.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Final_.Net.Areas.Admin.Services.Concrete
{
    public class AccountService : IAccountService
    {
        private readonly ModelStateDictionary _modelstate;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountService(UserManager<User> userManager,
            SignInManager<User> signInManager ,
            IActionContextAccessor actionContextAccessor)
        {
            _modelstate = actionContextAccessor.ActionContext.ModelState;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<AccountLoginVM> LoginAsync(AccountLoginVM model)
        {
            if (!_modelstate.IsValid) return null;
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                _modelstate.AddModelError(string.Empty, "UserName or Password is incorrect!!");
                return null;
            }
            if (!await _userManager.IsInRoleAsync(user, UserRoles.Admin.ToString()))
            {
                _modelstate.AddModelError(String.Empty, "UserName or Password is incorrect!!");
                return null;

            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!result.Succeeded)
            {
                _modelstate.AddModelError(String.Empty, "UserName or Password is incorrect!!");
                return null;
            }
            return model;
        }
    }
}
