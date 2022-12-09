using Core.Entities;
using DataAcces.Contexts;
using DataAcces.Repositories.Abstract;
using Final_.Net.Areas.Admin.Services.Abstract;
using Final_.Net.Areas.Admin.ViewModels.OurVision;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.IO;

namespace Final_.Net.Areas.Admin.Services.Concrete
{
    public class OurVisionService : IOurVisionService
    {
        private readonly ModelStateDictionary _modelstate;
        private readonly IOurVisionRepository _ourVisionRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public OurVisionService(IOurVisionRepository ourVisionRepository,
            IActionContextAccessor actionContextAccessor,
            IWebHostEnvironment webHostEnvironment
            )
        {
            _modelstate = actionContextAccessor.ActionContext.ModelState;
            _ourVisionRepository = ourVisionRepository;
            _webHostEnvironment = webHostEnvironment;

        }

        #region Create
        public async Task<bool> CreateAsync(OurVisionCreateVM model)
        {
            if (!_modelstate.IsValid) return false;

            if (model.Photo != null)
            {
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

            }

            var fileName = $"{Guid.NewGuid()}_{model.Photo.FileName}";
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "assets/images", fileName);
            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                await model.Photo.CopyToAsync(fileStream);
            }
            var ourvision = new OurVision
            {
                
                PhotoPath = path,
                Description = model.Description,
                CreateAt = DateTime.Now,
                Title = model.Title
            };
            await _ourVisionRepository.CreateAsync(ourvision);
            return true;
        }

        #endregion

        #region Update

        public async Task<OurVisionUpdateVM> GetUpdateModelAsync(int id)
        {
           if (!_modelstate.IsValid) return null;
            var ourVision = await _ourVisionRepository.GetAsync(id);
            
            if (ourVision == null)
            {
                _modelstate.AddModelError("Title", "Bele ourVision movcud deyil");
                return null;
            }
            var model = new OurVisionUpdateVM
            {
                Title = ourVision.Title,
                Description = ourVision.Description,
                PhotoName = ourVision.PhotoPath,
            };
            return model;
        }


        public async Task<bool> UpdateAsync(int id, OurVisionUpdateVM model)
        {
            if (!_modelstate.IsValid) return false;
            var ourVision = await _ourVisionRepository.GetAsync(model.Id);
            if (ourVision == null) return false;
            var photo = ourVision.PhotoPath;

            bool isExist = await _ourVisionRepository.AnyAsync(orv => orv.Title.ToLower().Trim() == model.Title.ToLower().Trim()
                                                            && orv.Id != model.Id);
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
                ourVision.PhotoPath = fileName;
            }
           // ourVision.PhotoPath = photo;
            ourVision.Description = model.Description;
            ourVision.Title = model.Title;
            ourVision.ModifiedAt = DateTime.Now;
            await _ourVisionRepository.SaveChanges();
            return true;
        }
        #endregion


        #region Delete
        public async Task<OurVision> GetDeleteAsync(int id)
        {
            var ourvision =await _ourVisionRepository.GetAsync(id);
            if (ourvision == null) return null;
            return ourvision;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ourvision = await _ourVisionRepository.GetAsync(id);
            if (ourvision == null) return false;
            if (System.IO.File.Exists(ourvision.PhotoPath))
            {
                System.IO.File.Delete(ourvision.PhotoPath);
            }

           await _ourVisionRepository.DeleteAsync(ourvision);
            return true;
        }
        #endregion


        #region Details
        public async Task<OurVision> DetailsAsync(int id)
        {
            var ourvision = await _ourVisionRepository.GetAsync(id);
            if (ourvision == null) return null;
            return ourvision;
        }
        #endregion
    }
}
