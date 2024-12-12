using IdentityMessageProject.BusinessLayer.Abstract;
using IdentityMessageProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityMessageProject.ViewComponents.Profile
{
	public class _LeftUserCardInfo : ViewComponent
	{
		private readonly IMessageService _messageService;
		private readonly UserManager<AppUser> _userManager;

		public _LeftUserCardInfo(IMessageService messageService, UserManager<AppUser> userManager)
		{
			_messageService = messageService;
			_userManager = userManager;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var user = await _userManager.FindByNameAsync(User.Identity.Name);
			var userId = user.Id;

			// Viewbags
			var values = _messageService.TGetAll().Where(x => x.ReceiverId == userId && x.IsDelete == false).ToList();
			var values2 = _messageService.TGetAll().Where(x => x.SenderId == userId && x.IsDelete == false).ToList();

			ViewBag.messageCount = values.Count();

			ViewBag.messageSendCount = values2.Count();

			return View();
		}
	}
}
