using Core.Entities;
using Core.Utilities;
using DataAcces.Repositories.Abstract;
using Final_.Net.Areas.Admin.Services.Abstract;
using Final_.Net.Areas.Admin.ViewModels.MedicalDepartments;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Final_.Net.Areas.Admin.Services.Concrete
{
    public class MedicalDepartmentsService : IMedicalDepartmentsService
    {
        private readonly ModelStateDictionary _modelstate;
        private readonly IMedicalDepartmentsRepository _medicalDepartmentsRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;

        public MedicalDepartmentsService(IActionContextAccessor actionContextAccessor,
            IMedicalDepartmentsRepository medicalDepartmentsRepository,
            IWebHostEnvironment webHostEnvironment,
            IFileService fileService)

        {
            _modelstate = actionContextAccessor.ActionContext.ModelState;
            _medicalDepartmentsRepository = medicalDepartmentsRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
        }
        public async Task<MedicalDepartmentsIndexVM> Index()
        {
            var model = new MedicalDepartmentsIndexVM
            {
                MedicalDepartments = await _medicalDepartmentsRepository.GetAllAsync(),
            };
            return model;
        }

        public async Task<bool> CreateAsync(MedicalDepartmentsCreateVM model)
        {
            if (!_modelstate.IsValid) return false;
            bool isExist = await _medicalDepartmentsRepository.AnyAsync(d =>
                           d.Title.ToLower().Trim() == model.Title.ToLower().Trim() &&
                           d.Id != model.Id);

            if (isExist)
            {
                _modelstate.AddModelError("Title", "bu adda product movcuddur!!");
                return false;
            }

            if (model.Photo != null)
            {
                if (!_fileService.IsImage(model.Photo))
                {
                    _modelstate.AddModelError("Photo", "Yuklenen sekil image formatinda olmalidir!!");
                    return false;
                }
                if (!_fileService.CheckSize(model.Photo, 60))
                {
                    _modelstate.AddModelError("Photo", "sekiln olcusu 90kbdan boyukdur!!");
                    return false;
                }
            }

           model.PhotoName=await _fileService.Upload(model.Photo, _webHostEnvironment.WebRootPath);
            var medicalDepartments = new MedicalDepartments
            {
                Description = model.Description,
                CreateAt = DateTime.Now,
                PhotoName = model.PhotoName,
                Title = model.Title,
            };
           await _medicalDepartmentsRepository.CreateAsync(medicalDepartments);
            return true;
        }

        public async Task<MedicalDepartmentsUpdateVM> UpdateAsync(int id)
        {
            var departments=await _medicalDepartmentsRepository.GetAsync(id);
            if (departments == null)
            {
                _modelstate.AddModelError("Title", "Bele ourVision movcud deyil");
                return null;
            }
            var model = new MedicalDepartmentsUpdateVM
            {
                Description = departments.Description,
                Title = departments.Title,
                PhotoName = departments.PhotoName,

            };
            return model;

        }

        public async Task<bool> UpdateAsync(int id, MedicalDepartmentsUpdateVM model)
        {
            if (!_modelstate.IsValid) return false;
            var departments = await _medicalDepartmentsRepository.GetAsync(id);
            if (departments == null)
            {
                _modelstate.AddModelError("Title", "Bele ourVision movcud deyil");
                return false;
            }

            if (model.Photo != null)
            {
               var fileName=await _fileService.Upload(model.Photo, _webHostEnvironment.WebRootPath);

                if (!_fileService.IsImage(model.Photo))
                {
                    _modelstate.AddModelError("Photo", "Yuklenen sekil image formatinda olmalidir!!");
                    return false;
                }

                if (!_fileService.CheckSize(model.Photo, 90))
                {
                    _modelstate.AddModelError("Photo", "sekiln olcusu 90kbdan boyukdur!!");
                    return false;
                }
                departments.PhotoName=fileName;
            }
            departments.ModifiedAt = DateTime.Now;
            departments.Title = model.Title;
            departments.Description = model.Description;
            await _medicalDepartmentsRepository.SaveChanges();
            return true;
        }

        public async Task<MedicalDepartments> GetDeleteAsync(int id)
        {
            var medicalDepartments=await _medicalDepartmentsRepository.GetAsync(id);
            if (medicalDepartments == null) return null;
            return medicalDepartments;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var medicalDepartments = await _medicalDepartmentsRepository.GetAsync(id);
            if (medicalDepartments == null) return false;
            _fileService.Delete(_webHostEnvironment.WebRootPath, medicalDepartments.PhotoName);
            await _medicalDepartmentsRepository.DeleteAsync(medicalDepartments);
            return true;
        }

        public async Task<MedicalDepartments> DetailsAsync(int id)
        {
            var departments = await _medicalDepartmentsRepository.GetAsync(id);
            if (departments == null) return null;
            return departments;
        }
    }
}
