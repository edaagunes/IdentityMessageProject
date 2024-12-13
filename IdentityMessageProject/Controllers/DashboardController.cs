using IdentityMessageProject.BusinessLayer.Abstract;
using IdentityMessageProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityMessageProject.Controllers
{
	public class DashboardController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IMessageService _messageService;
		private readonly IAppUserService _appUserService;

		public DashboardController(IAppUserService appUserService, IMessageService messageService, UserManager<AppUser> userManager)
		{
			_appUserService = appUserService;
			_messageService = messageService;
			_userManager = userManager;
		}

		public async Task<IActionResult> Index()
		{
			var user = await _userManager.FindByNameAsync(User.Identity.Name);

			var userId = _appUserService.TGetById(user.Id);

			var values = _messageService.TGetAll().Where(x => x.ReceiverId == userId.Id && x.IsDelete == false).ToList();
			var values2 = _messageService.TGetAll().Where(x => x.SenderId == userId.Id && x.IsDraft == true).ToList();
			var values3 = _messageService.TGetAll().Where(x => x.SenderId == userId.Id && x.IsDelete == false).ToList();

			var values4 = _messageService.TGetMessageWithAppUser(userId.Id).Sender;
			ViewBag.senderName = values4.Name + " " + values4.Surname;
			ViewBag.senderImage = values4.ImageUrl;
			ViewBag.senderEmail = values4.Email;

			ViewBag.userCount = _appUserService.TGetAll().Count();
			ViewBag.userMessageCount = values.Count();
			ViewBag.userDraftMessageCount = values2.Count();
			ViewBag.userSendMessageCount = values3.Count();

			return View();
		}
	}
}
