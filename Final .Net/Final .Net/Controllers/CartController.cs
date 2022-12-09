using Microsoft.AspNetCore.Mvc;

namespace Final_.Net.Controllers
{
	public class CartController : Controller
	{
		public async Task<IActionResult> Index()
		{
			return View();
		}
	}
}
