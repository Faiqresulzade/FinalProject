using Core.Entities;
using Final_.Net.Areas.Admin.ViewModels.OurVision;

namespace Final_.Net.Areas.Admin.Services.Abstract
{
    public interface IOurVisionService
    {
        Task<bool>CreateAsync(OurVisionCreateVM model);
        Task<OurVisionUpdateVM> GetUpdateModelAsync(int id);
        Task<bool>UpdateAsync(int id,OurVisionUpdateVM model);
        Task<OurVision>GetDeleteAsync(int id);
        Task<bool>DeleteAsync(int id);
        Task<OurVision> DetailsAsync(int id);
    }
}
