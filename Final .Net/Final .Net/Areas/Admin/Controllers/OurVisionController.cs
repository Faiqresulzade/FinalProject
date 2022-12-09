using DataAcces.Contexts;
using Final_.Net.Areas.Admin.Services.Abstract;
using Final_.Net.Areas.Admin.ViewModels.OurVision;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Final_.Net.Areas.Admin.Controllers
{
        [Area("admin")]
    public class OurVisionController:Controller
    {
        
        private readonly AppDbContext _appDbContext;
        private readonly IOurVisionService _ourVisionService;

        //  private readonly IOurVisionService _ourVisionService;

        public OurVisionController(AppDbContext appDbContext,
            IOurVisionService ourVisionService
            )
        {
            
            _appDbContext = appDbContext;
            _ourVisionService = ourVisionService;
            // _ourVisionService = ourVisionService;
        }
        public async Task<IActionResult> Index()
        {
            var model = new OurVisionIndexVM
            {
                OurVisions = await _appDbContext.OurVisions.ToListAsync(),
            };
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(OurVisionCreateVM model)
        {
            var succesed = await _ourVisionService.CreateAsync(model);
            if (!succesed) return View(model);
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult>Update(OurVisionUpdateVM model)
        {
            var mymodel = await _ourVisionService.GetUpdateModelAsync(model.Id);
            if(mymodel==null) return NotFound();
            return View(mymodel);
        }
        [HttpPost]
        public async Task<IActionResult>Update(int id, OurVisionUpdateVM model)
        {
            var myModel=await _ourVisionService.UpdateAsync(id, model);
            if (myModel) return RedirectToAction("Index");
            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult>Delete(int id)
        {
            var ourvision =await _ourVisionService.GetDeleteAsync(id);
            if(ourvision==null) return NotFound();
            return View(ourvision);
        }
        [HttpPost]
        public async Task<IActionResult>Deletee(int id)
        {
            var ourvision = await _ourVisionService.DeleteAsync(id);
            if (ourvision) return RedirectToAction("index");
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var ourvision = await _ourVisionService.DetailsAsync(id);
            if (ourvision == null) return NotFound();
            return View(ourvision);
        }
    }
}
