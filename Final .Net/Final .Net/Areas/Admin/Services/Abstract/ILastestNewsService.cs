using Core.Entities;
using Final_.Net.Areas.Admin.ViewModels.LastestNews;

namespace Final_.Net.Areas.Admin.Services.Abstract
{
    public interface ILastestNewsService
    {
        Task<LastestNewsIndexVM> IndexAsync();
        Task<bool> CreateAsync(LastestNewsCreateVM model);
        Task<LastestNewsUpdateVM> UpdateAsync(int id);
        Task<bool> UpdateAsync(LastestNewsUpdateVM model,int id);
        Task<LastestNews> GetDeleteAsync(int id);
        Task<bool> DeleteeAsync(int id);
        Task<LastestNews> Details(int id);
    }
}
