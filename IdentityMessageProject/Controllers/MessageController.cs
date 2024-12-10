using FluentValidation.Results;
using IdentityMessageProject.BusinessLayer.Abstract;
using IdentityMessageProject.BusinessLayer.ValidationRules;
using IdentityMessageProject.EntityLayer.Concrete;
using IdentityMessageProject.Models.Message;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
			_messageService.TChangeIsReadStatus(id);

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

		public IActionResult ChangeReadStatus(int id)
		{
			_messageService.TChangeIsReadStatus(id);
			return RedirectToAction("Inbox");
		}

		[HttpGet]
		public IActionResult NewMessage()
		{
			var userList = _appUserService.TGetAll();

			ViewBag.users=userList.Select(x=> new SelectListItem
			{
				Text =x.Name + " " + x.Surname,
				Value = x.Id.ToString()
			}).ToList();

			var firstUser=userList.FirstOrDefault();
			ViewBag.firstUser=firstUser;

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> NewMessage(Message message)
		{
			ModelState.Clear();

			var userList = _appUserService.TGetAll();
			ViewBag.users = userList.Select(x => new SelectListItem
			{
				Text = x.Name + " " + x.Surname,
				Value = x.Id.ToString()
			}).ToList();

			var user = await _userManager.FindByNameAsync(User.Identity.Name);
			var userId = _appUserService.TGetById(user.Id);

			message.SenderId = userId.Id;

			message.CreatedDate = DateTime.Now;

			NewMessageValidator validationRules = new NewMessageValidator();
			ValidationResult result=validationRules.Validate(message);

			if (result.IsValid)
			{
				_messageService.TInsert(message);
				return RedirectToAction("Inbox");
			}

			else
			{
				foreach (var item in result.Errors)
				{
					ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
				}
			}
			return View();
		}
	
	}
}
