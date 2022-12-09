using Final_.Net.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Final_.Net.Controllers
{
    public class DepartmentsController:Controller
    {
        private readonly IDepartmentsService _departmentsService;

        public DepartmentsController(IDepartmentsService departmentsService)
        {
            _departmentsService = departmentsService;
        }
        public async Task<IActionResult> Index()
        {
            var departments=await _departmentsService.IndexAsync();
            if (departments != null) return View(departments);
            return NotFound();
        }
    }
}
