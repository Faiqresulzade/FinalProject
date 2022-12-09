using Core.Entities;
using Final_.Net.Areas.Admin.ViewModels.Account;
using Final_.Net.Areas.Admin.ViewModels.OurVision;

namespace Final_.Net.Areas.Admin.Services.Abstract
{
    public interface IAccountService
    {
       Task<AccountLoginVM> LoginAsync(AccountLoginVM model);
    }
}
