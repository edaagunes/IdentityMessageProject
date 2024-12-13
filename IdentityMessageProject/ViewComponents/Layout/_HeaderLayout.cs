using IdentityMessageProject.BusinessLayer.Abstract;
using IdentityMessageProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityMessageProject.ViewComponents.Layout
{
	public class _HeaderLayout : ViewComponent
	{
		private readonly IMessageService _messageService;
		private readonly IAppUserService _appUserService;
		private readonly UserManager<AppUser> _userManager;

		public _HeaderLayout(UserManager<AppUser> userManager, IMessageService messageService, IAppUserService appUserService)
		{
			_userManager = userManager;
			_messageService = messageService;
			_appUserService = appUserService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var user = await _userManager.FindByNameAsync(User.Identity.Name);
			var userId = user.Id;

			user=_appUserService.TGetById(userId);

			ViewBag.userImage = user.ImageUrl;
			ViewBag.messages = user.ReceivedMessages.Count();

			return View();
		}
	}
}
