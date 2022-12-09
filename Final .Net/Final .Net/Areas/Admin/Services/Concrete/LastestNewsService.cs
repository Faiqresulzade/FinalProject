using Core.Entities;
using Core.Utilities;
using DataAcces.Repositories.Abstract;
using DataAcces.Repositories.Concrete;
using Final_.Net.Areas.Admin.Services.Abstract;
using Final_.Net.Areas.Admin.ViewModels.LastestNews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NuGet.Versioning;

namespace Final_.Net.Areas.Admin.Services.Concrete
{
    public class LastestNewsService : ILastestNewsService
    {
        private readonly ModelStateDictionary _modelstate;
        private readonly ILastestNewsRepository _lastestNewsRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;

        public LastestNewsService(IActionContextAccessor actionContextAccessor,
           ILastestNewsRepository lastestNewsRepository,
            IWebHostEnvironment webHostEnvironment,
            IFileService fileService)
        {
            _modelstate = actionContextAccessor.ActionContext.ModelState;
            _lastestNewsRepository = lastestNewsRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
        }


        public async Task<LastestNewsIndexVM> IndexAsync()
        {
            var model = new LastestNewsIndexVM
            {
                LastestNews = await _lastestNewsRepository.GetAllAsync()
            };
            return model;

        }
        #region Create
        public async Task<bool> CreateAsync(LastestNewsCreateVM model)
        {
            if (!_modelstate.IsValid) return false;
            bool isExist = await _lastestNewsRepository.AnyAsync(d =>
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
                model.PhotoPath = await _fileService.Upload(model.Photo, _webHostEnvironment.WebRootPath);
            }

            var lastestNews = new LastestNews
            {
                CreateAt = DateTime.Now,
                Description = model.Description,
                PhotoPath = model.PhotoPath,
                Title = model.Title,
                Time = model.Time
            };
            await _lastestNewsRepository.CreateAsync(lastestNews);
            return true;
        }
        #endregion

        public async Task<LastestNewsUpdateVM> UpdateAsync(int id)
        {
            if (!_modelstate.IsValid) return null;
            var lastestNews = await _lastestNewsRepository.GetAsync(id);
            if (lastestNews == null)
            {
                _modelstate.AddModelError("Title", "Bele ourVision movcud deyil");
                return null;

            }
            var model = new LastestNewsUpdateVM
            {
                Description =lastestNews.Description,
                Time=lastestNews.Time,
                Title= lastestNews.Title,
                PhotoPath=lastestNews.PhotoPath,
                
            };
            return model;
        }

        public async Task<bool> UpdateAsync(LastestNewsUpdateVM model, int id)
        {
            if (!_modelstate.IsValid) return false;

            var lastestnews=await _lastestNewsRepository.GetAsync(id);
            if (lastestnews == null)
            {
                _modelstate.AddModelError("Title", "Bele ourVision movcud deyil");
                return false;
            }
            bool isExist = await _lastestNewsRepository.AnyAsync(l => l.Title.ToLower().Trim() == lastestnews.Title.ToLower().Trim() &&
                                                                l.Id != lastestnews.Id);

            if (model.Photo != null)
            {
                var fileName = await _fileService.Upload(model.Photo, _webHostEnvironment.WebRootPath);

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
                lastestnews.PhotoPath = fileName;
            }
            lastestnews.ModifiedAt = DateTime.Now;
            lastestnews.Title = model.Title;
            lastestnews.Time = model.Time;
            lastestnews.Description = model.Description;
            await _lastestNewsRepository.SaveChanges();
            return true;
        }

        public async Task<LastestNews> GetDeleteAsync(int id)
        {
            return await _lastestNewsRepository.GetAsync(id);
        }

        public async Task<bool> DeleteeAsync(int id)
        {
            var lastestNews=await _lastestNewsRepository.GetAsync(id);
            if (lastestNews == null) return false;
            _fileService.Delete(_webHostEnvironment.WebRootPath, lastestNews.PhotoPath);
            await _lastestNewsRepository.DeleteAsync(lastestNews);
            return true;

        }

        public async Task<LastestNews> Details(int id)
        {
            return await _lastestNewsRepository.GetAsync(id);

        }
    }
}
