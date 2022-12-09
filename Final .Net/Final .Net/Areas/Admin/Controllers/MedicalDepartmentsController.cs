using DataAcces.Contexts;
using Final_.Net.Areas.Admin.Services.Abstract;
using Final_.Net.Areas.Admin.ViewModels.MedicalDepartments;
using Microsoft.AspNetCore.Mvc;

namespace Final_.Net.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MedicalDepartmentsController:Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMedicalDepartmentsService _medicalDepartmentsService;

        public MedicalDepartmentsController(AppDbContext appDbContext,
            IMedicalDepartmentsService medicalDepartmentsService
            )
        {
            _appDbContext = appDbContext;
            _medicalDepartmentsService = medicalDepartmentsService;
        }
        public async Task<IActionResult> Index()
        {
            var IsSucces = await _medicalDepartmentsService.Index();
            if(IsSucces!=null)return View(IsSucces);
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(MedicalDepartmentsCreateVM model)
        {
            bool isExist = await _medicalDepartmentsService.CreateAsync(model);
            if (isExist) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult> Update(int id)
        {
            var isExist = await _medicalDepartmentsService.UpdateAsync(id);
            if (isExist != null) return View(isExist);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, MedicalDepartmentsUpdateVM model)
        {

           var isExist=await _medicalDepartmentsService.UpdateAsync(id, model);
            if (isExist) return RedirectToAction(nameof(Index));
            return View(isExist);
        }

        [HttpGet]

        public async Task<IActionResult>Delete(int id)
        {
            var departments=await _medicalDepartmentsService.GetDeleteAsync(id);
            if (departments!=null) return View(departments);
            return NotFound();
        }

        [HttpPost]

        public async Task<IActionResult>Deletee(int id)
        {
           var departments= await _medicalDepartmentsService.DeleteAsync(id);
            if(departments) return RedirectToAction(nameof(Index));
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult>Details(int id)
        {
            var departments = await _medicalDepartmentsService.DetailsAsync(id);
            if(departments!=null)return View(departments);
            return NotFound();
        }
    }
}
