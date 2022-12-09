using Core.Entities;
using Core.Utilities;
using DataAcces.Repositories.Abstract;
using Final_.Net.Areas.Admin.Services.Abstract;
using Final_.Net.Areas.Admin.ViewModels.Doctors;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Final_.Net.Areas.Admin.Services.Concrete
{
    public class DoctorsService:IDoctorsService
    {
        private readonly ModelStateDictionary _modelstate;
        private readonly IDoctorsRepository _doctorsRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;

        public DoctorsService(IActionContextAccessor actionContextAccessor,
            IDoctorsRepository doctorsRepository,
            IWebHostEnvironment webHostEnvironment,
            IFileService fileService)
        {
            _modelstate = actionContextAccessor.ActionContext.ModelState;
            _doctorsRepository = doctorsRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
        }
        public async Task<DoctorsIndexVM> Index()
        {
            var model = new DoctorsIndexVM
            {
                Doctors = await _doctorsRepository.GetAllAsync(),
            };
            return model;

        }
        public async Task<bool> CreateAsync(DoctorsCreateVM model)
        {
            if (!_modelstate.IsValid) return false;
            bool isExist=await _doctorsRepository.AnyAsync(dr=>dr.Title.ToLower().Trim() == model.Title.ToLower().Trim()&&
                                                            dr.Id!=model.Id);
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
                    _modelstate.AddModelError("Photo", "sekiln olcusu 60kbdan boyukdur!!");
                    return false;
                }
            }
            model.PhotoName = await _fileService.Upload(model.Photo, _webHostEnvironment.WebRootPath);

            var doctor = new Doctors
            {
                CreateAt = DateTime.Now,
                FacebookLink = model.FacebookLink,
                TwitterLink = model.TwitterLink,
                LinkedinLink = model.LinkedinLink,
                Name = model.Name,
                Position = model.Position,
                Title = model.Title,
                PhotoName=model.PhotoName,
                
            };
            await _doctorsRepository.CreateAsync(doctor);
            return true;
        }

        public async Task<DoctorsUpdateVM> Update(int id)
        {
            if (!_modelstate.IsValid) return null;
            var doctors = await _doctorsRepository.GetAsync(id);
            if (doctors == null)
            {
                _modelstate.AddModelError("Title", "Bele ourVision movcud deyil");
                return null;
            }
            var model = new DoctorsUpdateVM
            {
                FacebookLink = doctors.FacebookLink,
                LinkedinLink = doctors.LinkedinLink,
                Name = doctors.Name,
                PhotoName = doctors.PhotoName,
                Position = doctors.Position,
                Title = doctors.Title,
                TwitterLink = doctors.TwitterLink,
            };
            return model;
        }

        public async Task<bool> Update(int id, DoctorsUpdateVM model)
        {
            if (!_modelstate.IsValid) return false;
            var doctors = await _doctorsRepository.GetAsync(id);
            if(doctors == null)return false;
            bool isExist=await _doctorsRepository.AnyAsync(d=>d.Title.ToLower().Trim()==model.Title.ToLower().Trim()&&
                                                            d.Id!=model.Id);

            if (isExist)
            {
                _modelstate.AddModelError("Title", "bu adda product movcuddur!!");
                return false;
            }

            if (model.Photo != null)
            {
                var fileName = $"{Guid.NewGuid()}_{model.Photo.FileName}";
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "assets/images", fileName);
                using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                {
                    await model.Photo.CopyToAsync(fileStream);
                }
                if (!model.Photo.ContentType.Contains("image/"))
                {
                    _modelstate.AddModelError("Photo", "Yuklenen sekil image formatinda olmalidir!!");
                    return false;
                }
                if (model.Photo.Length / 1024 > 90)
                {
                    _modelstate.AddModelError("Photo", "sekiln olcusu 90kbdan boyukdur!!");
                    return false;
                }
                doctors.PhotoName = fileName;
            }
            doctors.Name = model.Name;
            doctors.ModifiedAt = model.ModifiedAt;
            doctors.TwitterLink = model.TwitterLink;
            doctors.LinkedinLink=model.LinkedinLink;
            doctors.Title = model.Title;
            doctors.Position = model.Position;
            doctors.FacebookLink=model.FacebookLink;
            doctors.Title=model.Title;
            await _doctorsRepository.SaveChanges();
            return true;
        }

        public async Task<Doctors> DeleteAsync(int id)
        {
            var doctor = await _doctorsRepository.GetAsync(id);
            if (doctor == null) return null;
            return doctor;

        }

        public async Task<bool> DeleteeAsync(int id)
        {
            var doctor = await _doctorsRepository.GetAsync(id);
            if (doctor == null) return false;
            _fileService.Delete(_webHostEnvironment.WebRootPath, doctor.PhotoName);
            await _doctorsRepository.DeleteAsync(doctor);
            return true;
        }

        public async Task<Doctors> Details(int id)
        {
            var doctor = await _doctorsRepository.GetAsync(id);
            if (doctor == null) return null;
            return doctor;
        }
    }
}
