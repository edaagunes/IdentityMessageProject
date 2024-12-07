using Microsoft.AspNetCore.Mvc;

namespace IdentityMessageProject.Controllers
{
	public class MessageController : Controller
	{
		public IActionResult Inbox()
		{
			return View();
		}
	}
}
