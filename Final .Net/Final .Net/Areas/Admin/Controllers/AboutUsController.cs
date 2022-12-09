using DataAcces.Contexts;
using Final_.Net.Areas.Admin.Services.Abstract;
using Final_.Net.Areas.Admin.ViewModels.AboutUs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_.Net.Areas.Admin.Controllers
{
    [Area("admin")]
    public class AboutUsController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IAboutUsService _aboutUsService;

        public AboutUsController(AppDbContext appDbContext,
            IAboutUsService aboutUsService
            )
        {
            _appDbContext = appDbContext;
            _aboutUsService = aboutUsService;
        }
        public async Task<IActionResult> Index()
        {
            var aboutUs = await _aboutUsService.IndexAsync();
            if (aboutUs != null) return View(aboutUs);
            return NotFound();
        }

        [HttpGet]

        public async Task<IActionResult> Create()
        {
            var aboutUs = await _appDbContext.GetAbouts.FirstOrDefaultAsync();
            if (aboutUs == null)
            {
                return View();
            }
            return NotFound();
        }

        [HttpPost]

        public async Task<IActionResult> Create(AboutUsCreateVM model)
        {
            var succesed = await _aboutUsService.CreateAsync(model);
            if (succesed) return RedirectToAction("index");
            return View(model);
        }
        [HttpGet]

        public async Task<IActionResult>Update(int id)
        {
            var aboutUs =await _aboutUsService.GetUpdateModelAsync(id);
            if (aboutUs != null) return View(aboutUs);
            return NotFound();
        }

        [HttpPost]

        public async Task<IActionResult>Update(AboutUsUpdateVM model, int id)
        {
            var aboutUs = await _aboutUsService.UpdateAsync(id, model);
            if (aboutUs != null) return RedirectToAction(nameof(Index));
            return NotFound();
        }

        [HttpGet]

        public async Task<IActionResult>Delete(int id)
        {
            var aboutUs = await _aboutUsService.GetDeleteAsync(id);
            if (aboutUs == null) return NotFound();
            return View(aboutUs);
        }

        [HttpPost]

        public async Task<IActionResult>Deletee(int id)
        {
            var aboutUs = await _aboutUsService.DeleteAsync(id);
            if(aboutUs) return RedirectToAction(nameof(Index));
            return View(aboutUs);
        }
    }
}
