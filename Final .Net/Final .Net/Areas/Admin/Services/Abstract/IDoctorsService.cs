using Core.Entities;
using Final_.Net.Areas.Admin.ViewModels.Doctors;
using Final_.Net.Areas.Admin.ViewModels.MedicalDepartments;

namespace Final_.Net.Areas.Admin.Services.Abstract
{
    public interface IDoctorsService
    {
        Task<DoctorsIndexVM> Index();
        Task<bool> CreateAsync(DoctorsCreateVM model);
        Task<DoctorsUpdateVM>Update(int id);
        Task<bool>Update(int id,DoctorsUpdateVM model);
        Task<Doctors> DeleteAsync(int id);
        Task<bool> DeleteeAsync(int id);
        Task<Doctors> Details(int id);
    }
}
