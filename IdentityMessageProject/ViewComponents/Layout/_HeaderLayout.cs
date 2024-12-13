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
			var userId = _appUserService.TGetById(user.Id);


			ViewBag.userImage = user.ImageUrl;
			var values = _messageService.TGetAll().Where(x => x.ReceiverId == userId.Id && x.IsDelete == false).ToList();

			ViewBag.messages = values.Count();

			return View();
		}
	}
}
