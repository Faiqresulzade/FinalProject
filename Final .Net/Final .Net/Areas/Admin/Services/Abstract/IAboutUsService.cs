using Core.Entities;
using Final_.Net.Areas.Admin.ViewModels.AboutUs;
using Final_.Net.Areas.Admin.ViewModels.OurVision;

namespace Final_.Net.Areas.Admin.Services.Abstract
{
    public interface IAboutUsService
    {

        Task<AboutUsIndexVM> IndexAsync();
        Task<bool> CreateAsync(AboutUsCreateVM model);
        Task<AboutUsUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(int id, AboutUsUpdateVM model);
        Task<AboutUs> GetDeleteAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
