using Core.Entities;
using Final_.Net.Areas.Admin.ViewModels.Pricing;

namespace Final_.Net.Areas.Admin.Services.Abstract
{
    public interface IPricingService
    {
        Task<PricingIndexVM> IndexAsync(PricingIndexVM model);
        Task<bool> CreateAsync(PricingCreateVM model);
        Task<PricingUpdateVM> UpdateAsync(int id);
        Task<bool> UpdateAsync(PricingUpdateVM model);
        Task<Pricing> GetDeleteAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<Pricing>DetailsAsync(int id);
        IQueryable<Pricing> FilterbyTitle(string title);
    }
}
