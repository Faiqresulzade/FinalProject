using Core.Entities;
using Final_.Net.Areas.Admin.ViewModels.WhyChoose;

namespace Final_.Net.Areas.Admin.Services.Abstract
{
    public interface IWhyChooseService
    {
        Task<WhyChooseIndexVM> IndexAsync();
        Task<WhyChoose> CreateAsync();
        Task<bool> CreateAsync(WhyChooseCreateVM model);
        Task<WhyChooseUpdateVM> UpdateAsync(int id);
        Task<bool> UpdateAsync(WhyChooseUpdateVM model ,int id);
        Task<WhyChoose> DeleteAsync(int id);
        Task<bool> DeleteeAsync(int id);
        Task<WhyChoose> Details(int id);
    }
}
