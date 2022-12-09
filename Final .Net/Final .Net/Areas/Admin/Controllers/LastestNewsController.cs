using Final_.Net.Areas.Admin.Services.Abstract;
using Final_.Net.Areas.Admin.Services.Concrete;
using Final_.Net.Areas.Admin.ViewModels.LastestNews;
using Microsoft.AspNetCore.Mvc;

namespace Final_.Net.Areas.Admin.Controllers
{
    [Area("admin")]
    public class LastestNewsController : Controller
    {
        private readonly ILastestNewsService _lastestNewsService;

        public LastestNewsController(ILastestNewsService lastestNewsService)
        {
            _lastestNewsService = lastestNewsService;
        }
        public async Task<IActionResult> Index()
        {
           var whyChoose=await _lastestNewsService.IndexAsync();
            if (whyChoose == null) return NotFound();
            return View(whyChoose);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LastestNewsCreateVM model)
        {
            bool isExist = await _lastestNewsService.CreateAsync(model);
            if (isExist) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult> Update(int id)
        {
            var lastestNews = await _lastestNewsService.UpdateAsync(id);
            if(lastestNews == null) return NotFound();
            return View(lastestNews);
        }

        [HttpPost]
        public async Task<IActionResult> Update(LastestNewsUpdateVM model,int id)
        {
            bool isExist=await _lastestNewsService.UpdateAsync(model,id);
            if(isExist) return RedirectToAction(nameof(Index));
            return NotFound();
        }

        [HttpGet]

        public async Task<IActionResult> Delete(int id)
        {
            var lastestNews=await _lastestNewsService.GetDeleteAsync(id);
            if (lastestNews != null) return View(lastestNews);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult>Deletee(int id)
        {
            bool isExist = await _lastestNewsService.DeleteeAsync(id);
            if (isExist) return RedirectToAction(nameof(Index));
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult>Details(int id)
        {
            var lastestNews = await _lastestNewsService.Details(id);
            if (lastestNews != null) return View(lastestNews);
            return NotFound();
        }
    }
}
