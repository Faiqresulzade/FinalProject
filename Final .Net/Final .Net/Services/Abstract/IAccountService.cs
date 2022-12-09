using Final_.Net.ViewModels.Account;

namespace Final_.Net.Services.Abstract
{
    public interface IAccountService
    {
        public Task<AccountRegisterVM> Register(AccountRegisterVM model);
        public Task<AccountLoginVM> Login(AccountLoginVM model);
    }
}
