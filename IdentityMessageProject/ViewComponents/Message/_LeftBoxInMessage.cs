using IdentityMessageProject.BusinessLayer.Abstract;
using IdentityMessageProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityMessageProject.ViewComponents.Message
{
	public class _LeftBoxInMessage : ViewComponent
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IMessageService _messageService;
		private readonly IAppUserService _appUserService;

		public _LeftBoxInMessage(UserManager<AppUser> userManager, IMessageService messageService, IAppUserService appUserService)
		{
			_userManager = userManager;
			_messageService = messageService;
			_appUserService = appUserService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var user = await _userManager.FindByNameAsync(User.Identity.Name);

			var userId = _appUserService.TGetById(user.Id);

			var values = _messageService.TGetAll().Where(x => x.ReceiverId == userId.Id && x.IsDelete==false).ToList();
			var values2 = _messageService.TGetAll().Where(x => x.SenderId == userId.Id && x.IsDelete == false).ToList();
			var values3 = _messageService.TGetAll().Where(x => (x.SenderId == userId.Id || x.ReceiverId == userId.Id) && x.IsDelete == true).ToList();

			ViewBag.messageCount=values.Count();

			ViewBag.messageSendCount=values2.Count();

			ViewBag.deleteMessageCount=values3.Count();

			return View();
		}
	}
}
