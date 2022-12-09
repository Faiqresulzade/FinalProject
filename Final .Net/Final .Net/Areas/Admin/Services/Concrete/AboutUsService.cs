using Core.Entities;
using DataAcces.Contexts;
using DataAcces.Repositories.Abstract;
using DataAcces.Repositories.Concrete;
using Final_.Net.Areas.Admin.Services.Abstract;
using Final_.Net.Areas.Admin.ViewModels.AboutUs;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Final_.Net.Areas.Admin.Services.Concrete
{
    public class AboutUsService : IAboutUsService
    {
        private readonly ModelStateDictionary _modelstate;
        private readonly IAboutUsRepository _aboutUsRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _appDbContext;

        public AboutUsService(IActionContextAccessor actionContextAccessor,
            IAboutUsRepository aboutUsRepository,
            IWebHostEnvironment webHostEnvironment,
            AppDbContext appDbContext
            )
        {
            _modelstate = actionContextAccessor.ActionContext.ModelState;
            _aboutUsRepository = aboutUsRepository;
            _webHostEnvironment = webHostEnvironment;
            _appDbContext = appDbContext;
        }
        public async Task<AboutUsIndexVM> IndexAsync()
        {
           var model = new AboutUsIndexVM
            {
                GetAboutUs = await _aboutUsRepository.FirstorDefaultAsync()
            };
            return model;
        }


        #region Create
        public async Task<bool> CreateAsync(AboutUsCreateVM model)
        {
            if (!_modelstate.IsValid) return false;
            bool isExist = await _aboutUsRepository.AnyAsync(orv => orv.Title.ToLower().Trim() == model.Title.ToLower().Trim());
            if (isExist)
            {
                _modelstate.AddModelError("Title", "bu adda product movcuddur!!");
                return false;
            }
            if (model.SignaturePhoto != null)
            {
                if (!model.SignaturePhoto.ContentType.Contains("image/"))
                {
                    _modelstate.AddModelError("Photo", "Yuklenen sekil image formatinda olmalidir!!");
                    return false;
                }
                if (model.SignaturePhoto.Length / 1024 > 90)
                {
                    _modelstate.AddModelError("Photo", "sekiln olcusu 90kbdan boyukdur!!");
                    return false;
                }
            }
            var fileName = $"{Guid.NewGuid()}_{model.SignaturePhoto.FileName}";
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "assets/images", fileName);
            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                await model.SignaturePhoto.CopyToAsync(fileStream);
            }

            if (model.SecondPhoto != null)
            {
                if (!model.SecondPhoto.ContentType.Contains("image/"))
                {
                    _modelstate.AddModelError("Photo", "Yuklenen sekil image formatinda olmalidir!!");
                    return false;
                }
                if (model.SecondPhoto.Length / 1024 > 90)
                {
                    _modelstate.AddModelError("Photo", "sekiln olcusu 90kbdan boyukdur!!");
                    return false;
                }
            }
            var secondfileName = $"{Guid.NewGuid()}_{model.SecondPhoto.FileName}";
            var seconphotopath = Path.Combine(_webHostEnvironment.WebRootPath, "assets/images", secondfileName);
            using (FileStream fileStream = new FileStream(seconphotopath, FileMode.Create, FileAccess.ReadWrite))
            {
                await model.SecondPhoto.CopyToAsync(fileStream);
            }

            if (model.MainPhoto != null)
            {
                if (!model.MainPhoto.ContentType.Contains("image/"))
                {
                    _modelstate.AddModelError("Photo", "Yuklenen sekil image formatinda olmalidir!!");
                    return false;
                }
                if (model.MainPhoto.Length / 1024 > 90)
                {
                    _modelstate.AddModelError("Photo", "sekiln olcusu 90kbdan boyukdur!!");
                    return false;
                }
            }
            var mainphotofileName = $"{Guid.NewGuid()}_{model.MainPhoto.FileName}";
            var mainphotopath = Path.Combine(_webHostEnvironment.WebRootPath, "assets/images", mainphotofileName);
            using (FileStream fileStream = new FileStream(mainphotopath, FileMode.Create, FileAccess.ReadWrite))
            {
                await model.MainPhoto.CopyToAsync(fileStream);
            }
            var aboutus = new AboutUs
            {
                CreateAt = DateTime.Now,
                SecondPhotoPath = secondfileName,
                MainPhotoPath = mainphotofileName,
                SignuturePath = fileName,
                Description = model.Description,
                Title = model.Title,
                SubTitle = model.SubTitle,
            };
            await _aboutUsRepository.CreateAsync(aboutus);
            return true;
        }

        #endregion
        #region Update
        public async Task<AboutUsUpdateVM> GetUpdateModelAsync(int id)
        {
           var aboutUs=await _aboutUsRepository.GetAsync(id);
            if (aboutUs == null)
            {
                _modelstate.AddModelError("Title", "Bele tittle movcud deyil!!");
                return null;
            }

            var model = new AboutUsUpdateVM
            {
                Description = aboutUs.Description,
                MainPhotoPath = aboutUs.MainPhotoPath,
                SecondPhotoPath = aboutUs.SecondPhotoPath,
                SignuturePath = aboutUs.SignuturePath,
                Title = aboutUs.Title,
                SubTitle = aboutUs.SubTitle
            };
            return model;
        }


        public async Task<bool> UpdateAsync(int id, AboutUsUpdateVM model)
        {
            if (!_modelstate.IsValid) return false;
            var aboutUs = await _aboutUsRepository.GetAsync(id);
            if (aboutUs == null) return false;

            bool isExist = await _aboutUsRepository
                                                .AnyAsync(abts => abts.Title.ToLower().Trim() == model.Title.ToLower().Trim() &&
                                                            abts.Id != model.Id);

            if (isExist)
            {
                _modelstate.AddModelError("Title", "bu adda product movcuddur!!");
                return false;
            }
            if (model.SignuturePath != null)
            {
                var fileName = $"{Guid.NewGuid()}_{model.SignaturePhoto.FileName}";
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "assets/images", fileName);
                using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                {
                    await model.SignaturePhoto.CopyToAsync(fileStream);
                }
                if (!model.SignaturePhoto.ContentType.Contains("image/"))
                {
                    _modelstate.AddModelError("Photo", "Yuklenen sekil image formatinda olmalidir!!");
                    return false;
                }
                if (model.SignaturePhoto.Length / 1024 > 90)
                {
                    _modelstate.AddModelError("Photo", "sekiln olcusu 90kbdan boyukdur!!");
                    return false;
                }
                aboutUs.SignuturePath = fileName;
            }

            if (model.MainPhoto != null)
            {
                var fileName = $"{Guid.NewGuid()}_{model.MainPhoto.FileName}";
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "assets/images", fileName);
                using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                {
                    await model.MainPhoto.CopyToAsync(fileStream);
                }
                if (!model.MainPhoto.ContentType.Contains("image/"))
                {
                    _modelstate.AddModelError("Photo", "Yuklenen sekil image formatinda olmalidir!!");
                    return false;
                }
                if (model.MainPhoto.Length / 1024 > 90)
                {
                    _modelstate.AddModelError("Photo", "sekiln olcusu 90kbdan boyukdur!!");
                    return false;
                }
                aboutUs.MainPhotoPath = fileName;
            }

            if (model.SecondPhoto != null)
            {
                var fileName = $"{Guid.NewGuid()}_{model.SecondPhoto.FileName}";
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "assets/images", fileName);
                using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                {
                    await model.SecondPhoto.CopyToAsync(fileStream);
                }
                if (!model.SecondPhoto.ContentType.Contains("image/"))
                {
                    _modelstate.AddModelError("Photo", "Yuklenen sekil image formatinda olmalidir!!");
                    return false;
                }
                if (model.SecondPhoto.Length / 1024 > 90)
                {
                    _modelstate.AddModelError("Photo", "sekiln olcusu 90kbdan boyukdur!!");
                    return false;
                }
                aboutUs.SecondPhotoPath = fileName;
            }
            aboutUs.SubTitle = model.SubTitle;
            aboutUs.Title = model.Title;
            aboutUs.ModifiedAt = DateTime.Now;
            aboutUs.Description=model.Description;
            await _aboutUsRepository.SaveChanges();
            return true;
        }

        #endregion

        #region Delete
        public async Task<AboutUs> GetDeleteAsync(int id)
        {
            var aboutUs =await _aboutUsRepository.GetAsync(id);
            if(aboutUs == null) return null;
            return aboutUs;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var aboutUs = await _aboutUsRepository.GetAsync(id);
            if (aboutUs == null) return false;
            if (System.IO.File.Exists(aboutUs.SecondPhotoPath))
            {
                System.IO.File.Delete(aboutUs.SecondPhotoPath);
            }
            if (System.IO.File.Exists(aboutUs.MainPhotoPath))
            {
                System.IO.File.Delete(aboutUs.MainPhotoPath);
            }
            if (System.IO.File.Exists(aboutUs.SignuturePath))
            {
                System.IO.File.Delete(aboutUs.SignuturePath);
            }

            await _aboutUsRepository.DeleteAsync(aboutUs);
            return true;
        }


        #endregion
    }
}
