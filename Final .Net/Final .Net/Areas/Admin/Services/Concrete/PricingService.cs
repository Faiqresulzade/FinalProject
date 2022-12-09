using Core.Entities;
using Core.Utilities;
using DataAcces.Repositories.Abstract;
using DataAcces.Repositories.Concrete;
using Final_.Net.Areas.Admin.Services.Abstract;
using Final_.Net.Areas.Admin.ViewModels.Pricing;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace Final_.Net.Areas.Admin.Services.Concrete
{
    public class PricingService : IPricingService
    {
        private readonly ModelStateDictionary _modelstate;
        private readonly IPricingRepository _pricingRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;

        public PricingService(IActionContextAccessor actionContextAccessor,
                                IPricingRepository pricingRepository,
                                IWebHostEnvironment webHostEnvironment,
                                IFileService fileService)
        {
            _modelstate = actionContextAccessor.ActionContext.ModelState;
            _pricingRepository = pricingRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
        }
        public async Task<PricingIndexVM> IndexAsync(PricingIndexVM model)
        {
            var pricing = FilterbyTitle(model.Title);
            model = new PricingIndexVM()
            {
                Pricings = await pricing.ToListAsync()
            };
            return model;
        }

        #region CRUD
        public async Task<bool> CreateAsync(PricingCreateVM model)
        {
            if (!_modelstate.IsValid) return false;
            bool isExist = await _pricingRepository.AnyAsync(d =>
                          d.Title.ToLower().Trim() == model.Title.ToLower().Trim() &&
                          d.Id != model.Id);

            if (isExist)
            {
                _modelstate.AddModelError("Title", "bu adda product movcuddur!!");
                return false;
            }

            var pricing = new Pricing
            {
                CreateAt = DateTime.Now,
                Description = model.Description,
                Price = model.Price,
                Text = model.Text,
                Title = model.Title,
                Status=model.Status
            };
            await _pricingRepository.CreateAsync(pricing);
            return true;
        }

        public async Task<PricingUpdateVM> UpdateAsync(int id)
        {
            if (!_modelstate.IsValid) return null;
            var pricing = await _pricingRepository.GetAsync(id);
            if (pricing == null)
            {
                _modelstate.AddModelError("Title", "Bele pricing movcud deyil");
                return null;
            }

            var model = new PricingUpdateVM
            {
                Description = pricing.Description,
                Price = pricing.Price,
                Text = pricing.Text,
                Title = pricing.Title,
                Status=pricing.Status
            };
            return model;
        }

        public async Task<bool> UpdateAsync(PricingUpdateVM model)
        {
            if (!_modelstate.IsValid) return false;

            var pricing = await _pricingRepository.GetAsync(model.Id);
            if (pricing == null)
            {
                _modelstate.AddModelError("Title", "Bele pricing movcud deyil");
                return false;
            }

            bool isExist = await _pricingRepository.AnyAsync(p => p.Title.ToLower().Trim() ==
                                               pricing.Title.ToLower().Trim() &&
                                               p.Id != pricing.Id);

            if (isExist)
            {
                _modelstate.AddModelError("Title", "bu adda pricing movcuddur!!");
                return false;
            }

            pricing.Title = model.Title;
            pricing.Price = model.Price;
            pricing.Title = model.Title;
            pricing.Text = model.Text;
            pricing.ModifiedAt = DateTime.Now;
            pricing.Status = model.Status;
            await _pricingRepository.SaveChanges();
            return true;
        }

        public async Task<Pricing> GetDeleteAsync(int id)
        {
            var pricing=await _pricingRepository.GetAsync(id);
            if (pricing == null) return null;
            return pricing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var pricing = await _pricingRepository.GetAsync(id);
            if (pricing == null) return false;
            await _pricingRepository.DeleteAsync(pricing);
            return true;
        }

        public async Task<Pricing> DetailsAsync(int id)
        {
            var pricing = await _pricingRepository.GetAsync(id);
            if (pricing == null) return null;
            return pricing;
        }


        #endregion

        #region Filter
        public IQueryable<Pricing> FilterbyTitle(string title)
        {
            return _pricingRepository.FilterbyTitle(title);
        }

        #endregion
    }
}
