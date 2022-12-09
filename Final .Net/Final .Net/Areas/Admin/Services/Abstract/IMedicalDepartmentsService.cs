using Core.Entities;
using Final_.Net.Areas.Admin.ViewModels.MedicalDepartments;

namespace Final_.Net.Areas.Admin.Services.Abstract
{
    public interface IMedicalDepartmentsService
    {
        Task<MedicalDepartmentsIndexVM> Index();
        Task<bool> CreateAsync(MedicalDepartmentsCreateVM model);
        Task<MedicalDepartmentsUpdateVM> UpdateAsync(int id);
        Task<bool> UpdateAsync(int id ,MedicalDepartmentsUpdateVM model);
        Task<MedicalDepartments> GetDeleteAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<MedicalDepartments> DetailsAsync(int id);
    }
}
