using Microsoft.AspNetCore.Mvc;

namespace IdentityMessageProject.ViewComponents.Layout
{
	public class _HeaderLayout : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
