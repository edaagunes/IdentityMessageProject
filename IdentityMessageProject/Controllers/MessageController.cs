using IdentityMessageProject.BusinessLayer.Abstract;
using IdentityMessageProject.EntityLayer.Concrete;
using IdentityMessageProject.Models.Message;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityMessageProject.Controllers
{
	public class MessageController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IMessageService _messageService;
		private readonly IAppUserService _appUserService;

		public MessageController(UserManager<AppUser> userManager, IMessageService messageService, IAppUserService appUserService)
		{
			_userManager = userManager;
			_messageService = messageService;
			_appUserService = appUserService;
		}

		public async Task<IActionResult> Inbox()
		{
			ViewBag.ActiveTab = "Inbox";

			var user = await _userManager.FindByNameAsync(User.Identity.Name);

			var userId = _appUserService.TGetById(user.Id);

			var values = _messageService.TGetAll().Where(x => x.ReceiverId == userId.Id).ToList();

			ViewBag.messageCount=values.Count;

			return View(values);
		}

		public async Task<IActionResult> MessageDetail(int id)
		{
			var message = _messageService.TGetMessageWithAppUser(id);

			return View(message);
		}

		public async Task<IActionResult> OutBox()
		{
			ViewBag.ActiveTab = "Outbox";

			var user = await _userManager.FindByNameAsync(User.Identity.Name);

			var userId = _appUserService.TGetById(user.Id);

			var values = _messageService.TGetAll().Where(x => x.SenderId == userId.Id).ToList();

			return View(values);
		}
	}
}
