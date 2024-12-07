using Microsoft.AspNetCore.Mvc;

namespace IdentityMessageProject.ViewComponents.Layout
{
	public class _HeadLayout : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
