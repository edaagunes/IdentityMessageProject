using Microsoft.AspNetCore.Mvc;

namespace IdentityMessageProject.ViewComponents.Layout
{
	public class _SidebarLayout : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
