using Final_.Net.Areas.Admin.Services.Abstract;
using Final_.Net.Areas.Admin.ViewModels.Doctors;
using Microsoft.AspNetCore.Mvc;

namespace Final_.Net.Areas.Admin.Controllers
{
    [Area("admin")]
    public class DoctorsController:Controller
    {
        private readonly IDoctorsService _doctorsService;

        public DoctorsController(IDoctorsService doctorsService)
        {
            _doctorsService = doctorsService;
        }
        public async Task<IActionResult> Index()
        {
            var doctors = await _doctorsService.Index();
            if(doctors == null) return NotFound();
            return View(doctors);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult>Create(DoctorsCreateVM model)
        {
            var isExist = await _doctorsService.CreateAsync(model);
            if (isExist) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var doctor = await _doctorsService.Update(id);
            if(doctor == null) return NotFound();
            return View(doctor);
        }
        [HttpPost]
        public async Task<IActionResult>Update(DoctorsUpdateVM model,int id)
        {
            var isExist = await _doctorsService.Update(id, model);
            if(isExist) return RedirectToAction(nameof(Index));
            return NotFound();
        }

        [HttpGet]

        public async Task<IActionResult>Delete(int id)
        {
            var doctor = await _doctorsService.DeleteAsync(id);
            if (doctor == null) return NotFound();
            return View(doctor);
        }

        [HttpPost]
        public async Task<IActionResult>Deletee(int id)
        {
            var isExist=await _doctorsService.DeleteeAsync(id);
            if(isExist) return RedirectToAction(nameof(Index));
            return NotFound();
            
        }

        [HttpGet]

        public async Task<IActionResult> Details(int id)
        {
            var doctor = await _doctorsService.Details(id);
            if(doctor!=null) return View(doctor);
            return NotFound();
        }
    }
}
