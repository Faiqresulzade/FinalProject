using Core.Entities;
using Core.Utilities;
using DataAcces.Repositories.Abstract;
using Final_.Net.Areas.Admin.Services.Abstract;
using Final_.Net.Areas.Admin.ViewModels.WhyChoose;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Final_.Net.Areas.Admin.Services.Concrete
{
    public class WhyChooseService : IWhyChooseService
    {
        private readonly ModelStateDictionary _modelstate;
        private readonly IWhyChooseRepository _whyChooseRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;

        public WhyChooseService(IActionContextAccessor actionContextAccessor,
                                IWhyChooseRepository whyChooseRepository,
                                IWebHostEnvironment webHostEnvironment,
                                IFileService fileService)
        {
            _modelstate = actionContextAccessor.ActionContext.ModelState;
            _whyChooseRepository = whyChooseRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
        }

      

        public async Task<WhyChooseIndexVM> IndexAsync()
        {
            var model = new WhyChooseIndexVM
            {
                WhyChoose =await _whyChooseRepository.FirstorDefaultAsync(),
            };
            return model;
        }
        public async Task<WhyChoose> CreateAsync()
        {
            var whyChoose=await _whyChooseRepository.FirstorDefaultAsync();
            return whyChoose;
        }

        public async Task<bool> CreateAsync(WhyChooseCreateVM model)
        {
            if (!_modelstate.IsValid) return false;
            bool isExist = await _whyChooseRepository.AnyAsync(w =>
                            w.Title.ToLower().Trim() ==
                            model.Title.ToLower().Trim() &&
                            w.Id != model.Id);
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
            model.PhotoPath = await _fileService.Upload(model.Photo, _webHostEnvironment.WebRootPath);

            var whyChoose = new WhyChoose
            {
                CreateAt = DateTime.Now,
                Descriprtion = model.Descriprtion,
                Title = model.Title,
                Text = model.Text,
                Photo = model.PhotoPath,
            };
            await _whyChooseRepository.CreateAsync(whyChoose);
            return true;
        }

        public async Task<WhyChooseUpdateVM> UpdateAsync(int id)
        {
            if (!_modelstate.IsValid) return null;
            var whyChoose=await _whyChooseRepository.GetAsync(id);
            if(whyChoose== null)
            {
                _modelstate.AddModelError("Title", "Bele tittle movcud deyil!!");
                return null;
            }
            var model = new WhyChooseUpdateVM
            {
                Descriprtion = whyChoose.Descriprtion,
                Text = whyChoose.Text,
                Title = whyChoose.Title,
                PhotoPath = whyChoose.Photo
            };
            return model;
        }

        public async Task<bool> UpdateAsync(WhyChooseUpdateVM model, int id)
        {
            if (!_modelstate.IsValid) return false;
            var whyChoose=await _whyChooseRepository.GetAsync(model.Id);
            if(whyChoose==null) return false;
            bool isExist = await _whyChooseRepository.AnyAsync(w => w.Title.ToLower().Trim() ==
                                                                model.Title.ToLower().Trim() &&
                                                                w.Id != model.Id);
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
                whyChoose.Photo = fileName;
            }
            whyChoose.Descriprtion=model.Descriprtion;
            whyChoose.Title = model.Title;
            whyChoose.Text = model.Text;
            whyChoose.ModifiedAt=DateTime.Now;
            await _whyChooseRepository.SaveChanges();
            return true;
        }

        public async Task<WhyChoose> DeleteAsync(int id)
        {
            return await _whyChooseRepository.GetAsync(id);
        }

        public async Task<bool> DeleteeAsync(int id)
        {
            var whyChoose=await _whyChooseRepository.GetAsync(id);
            if (whyChoose == null) return false;
            _fileService.Delete(_webHostEnvironment.WebRootPath, whyChoose.Photo);
            await _whyChooseRepository.DeleteAsync(whyChoose);
            return true;
        }

        public async Task<WhyChoose> Details(int id)
        {
            return await _whyChooseRepository.GetAsync(id);
        }
    }
}
