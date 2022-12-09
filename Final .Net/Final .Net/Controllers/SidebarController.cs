using Microsoft.AspNetCore.Mvc;

namespace Final_.Net.Controllers
{
	public class SidebarController : Controller
	{
		public async Task<IActionResult> Index()
		{
			return View();
		}
	}
}
