using Microsoft.AspNetCore.Mvc;

namespace IdentityMessageProject.Controllers
{
	public class ProfileController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
