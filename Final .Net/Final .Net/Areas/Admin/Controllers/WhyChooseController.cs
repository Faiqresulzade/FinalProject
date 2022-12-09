using DataAcces.Repositories.Abstract;
using Final_.Net.Areas.Admin.Services.Abstract;
using Final_.Net.Areas.Admin.ViewModels.WhyChoose;
using Microsoft.AspNetCore.Mvc;

namespace Final_.Net.Areas.Admin.Controllers
{
    [Area("admin")]
    public class WhyChooseController : Controller
    {
        private readonly IWhyChooseService _whyChooseService;

        public WhyChooseController(IWhyChooseService whyChooseService)
        {
            _whyChooseService = whyChooseService;
        }
        public async Task<IActionResult> Index()
        {
           var whyChoose=await _whyChooseService.IndexAsync();
            if(whyChoose!=null)return View(whyChoose);
            return NotFound();
        }

        public async Task<IActionResult> Create()
        {
           var whyChoose=await _whyChooseService.CreateAsync();
            if(whyChoose==null) return View();
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult>Create(WhyChooseCreateVM model)
        {
            bool isExist = await _whyChooseService.CreateAsync(model);
            if (isExist) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult>Update(int id)
        {
            var whyChoose=await _whyChooseService.UpdateAsync(id);
            if (whyChoose != null) return View(whyChoose);
            return NotFound();
        }

        [HttpPost]

        public async Task<IActionResult>Update(WhyChooseUpdateVM model, int id)
        {
            var isExist=await _whyChooseService.UpdateAsync(model,id);
            if(isExist) return RedirectToAction(nameof(Index));
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult>Delete(int id)
        {
            var whyChoose=await _whyChooseService.DeleteAsync(id);
            if(whyChoose!=null)return View(whyChoose);
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult>Deletee(int id)
        {
            bool isExist=await _whyChooseService.DeleteeAsync(id);
            if(isExist)return RedirectToAction(nameof(Index));
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var whyChoose = await _whyChooseService.Details(id);
            if (whyChoose != null) return View(whyChoose);
            return NotFound();
        }
    }
}
