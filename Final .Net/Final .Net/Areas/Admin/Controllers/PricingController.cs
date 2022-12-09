using DataAcces.Contexts;
using Final_.Net.Areas.Admin.Services.Abstract;
using Final_.Net.Areas.Admin.ViewModels.Pricing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Final_.Net.Areas.Admin.Controllers
{
    [Area("admin")]
    public class PricingController : Controller
    {
        private readonly IPricingService _pricingService;
        private readonly AppDbContext _appDbContext;

        public PricingController(IPricingService pricingService,
            AppDbContext appDbContext)
        {
            _pricingService = pricingService;
            _appDbContext = appDbContext;
        }
        public async Task<IActionResult> Index(PricingIndexVM model)
        {
            var pricing = await _pricingService.IndexAsync(model);
            if (pricing != null) return View(pricing);
            return NotFound();
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PricingCreateVM model)
        {
            bool isExist = await _pricingService.CreateAsync(model);
            if(isExist)return RedirectToAction(nameof(Index));
            return View(model);
        }

        public async Task<IActionResult> Update(int id)
        {
            var pricing=await _pricingService.UpdateAsync(id);
            if (pricing != null) return View(pricing);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(PricingUpdateVM model,int id)
        {
            if (model.Id != id) return BadRequest();
            bool isExist = await _pricingService.UpdateAsync(model);
            if (isExist) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult>Delete(int id)
        {
            var pricing = await _pricingService.GetDeleteAsync(id);
            if(pricing != null) return View(pricing);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult>Deletee(int id)
        {
            bool isExist=await _pricingService.DeleteAsync(id);
            if (isExist) return RedirectToAction(nameof(Index));
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult>Details(int id)
        {
            var pricing = await _pricingService.DetailsAsync(id);
            if (pricing != null) return View(pricing);
            return NotFound();
        }
    }
}
