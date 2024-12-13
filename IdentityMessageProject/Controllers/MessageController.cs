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

			var values = _messageService.TGetAll().Where(x => x.ReceiverId == userId.Id && x.IsDelete == false).ToList();


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

			var values = _messageService.TGetAll().Where(x => x.SenderId == userId.Id && x.IsDelete == false).ToList();

			return View(values);
		}

		public IActionResult ChangeReadStatus(int id)
		{
			_messageService.TChangeIsReadStatus(id);
			return RedirectToAction("Inbox");
		}

		[HttpGet]
		public IActionResult NewMessage(int? id)
		{
			var userList = _appUserService.TGetAll();

			ViewBag.users = userList.Select(x => new SelectListItem
			{
				Text = x.Name + " " + x.Surname,
				Value = x.Id.ToString()
			}).ToList();


			if (id.HasValue)
			{
				var existingMessage = _messageService.TGetById(id.Value);
				return View(existingMessage); // Mevcut mesaj detaylarını gönder
			}


			return View();
		}

		[HttpPost]
		public async Task<IActionResult> NewMessage(Message message, string action)
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

			// Taslak Mesaj
			if (action == "SaveDraft")
			{
				if (message.MessageId == 0)
				{
					message.IsDraft = true;
					_messageService.TInsert(message);
				}
				else
				{
					var existingMessage = _messageService.TGetById(message.MessageId);

					if (existingMessage != null)
					{
						existingMessage.Title = message.Title;
						existingMessage.Content = message.Content;
						existingMessage.ReceiverId = message.ReceiverId;
						existingMessage.IsDraft = true;
						_messageService.TUpdate(existingMessage);
					}

				}

				return RedirectToAction("DraftMessageList");
			}

			else if (action == "NewMessage")
			{
				NewMessageValidator validationRules = new NewMessageValidator();
				ValidationResult result = validationRules.Validate(message);

				if (result.IsValid)
				{
					if (message.MessageId == 0)
					{
						// Yeni mesaj gönder
						message.IsDraft = false;
						_messageService.TInsert(message);
					}
					else
					{
						// Mevcut taslağı gönder
						var existingMessage = _messageService.TGetById(message.MessageId);
						existingMessage.Title = message.Title;
						existingMessage.Content = message.Content;
						existingMessage.ReceiverId = message.ReceiverId;
						existingMessage.IsDraft = false;
						existingMessage.CreatedDate = DateTime.Now;
						_messageService.TUpdate(existingMessage);
					}

					return RedirectToAction("Inbox");
				}
				else
				{
					foreach (var item in result.Errors)
					{
						ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
					}
				}
			}

			return View(message);
		}

		public IActionResult IsDeleteMessage(int id)
		{
			_messageService.TChangeIsDeleteStatus(id);
			return RedirectToAction("DeleteMessages");
		}

		public async Task<IActionResult> DeleteMessages()
		{
			ViewBag.ActiveTab = "Trash";

			var user = await _userManager.FindByNameAsync(User.Identity.Name);

			var userId = _appUserService.TGetById(user.Id);

			var values = _messageService.TGetAll().Where(x => (x.SenderId == userId.Id || x.ReceiverId == userId.Id) && x.IsDelete == true).ToList();

			return View(values);
		}

		public async Task<IActionResult> SaveDraft(Message message)
		{
			if (message.MessageId > 0)
			{
				// Mevcut taslağı güncelle
				var existingMessage = _messageService.TGetById(message.MessageId);

				existingMessage.Title = message.Title;
				existingMessage.Content = message.Content;
				existingMessage.ReceiverId = message.ReceiverId;
				existingMessage.IsDraft = true;
				_messageService.TUpdate(existingMessage);

			}
			else
			{
				// Yeni taslak ekle
				var user = await _userManager.FindByNameAsync(User.Identity.Name);
				var userId = _appUserService.TGetById(user.Id);

				message.SenderId = userId.Id;
				message.IsDraft = true;
				message.CreatedDate = DateTime.Now;
				_messageService.TInsert(message);
			}

			return RedirectToAction("DraftMessageList");
		}

		public async Task<IActionResult> DraftMessageList()
		{
			ViewBag.ActiveTab = "Draft";

			var user = await _userManager.FindByNameAsync(User.Identity.Name);

			var userId = _appUserService.TGetById(user.Id);

			var values = _messageService.TGetAll().Where(x => (x.SenderId == userId.Id || x.ReceiverId == userId.Id) && x.IsDraft == true).ToList();

			return View(values);
		}

		public async Task<IActionResult> DraftMessageDetail(int id)
		{
			var message = _messageService.TGetMessageWithAppUser(id);

			var userList = _appUserService.TGetAll();
			ViewBag.users = userList.Select(x => new SelectListItem
			{
				Text = x.Name + " " + x.Surname,
				Value = x.Id.ToString(),
				Selected = x.Id == message.ReceiverId
			}).ToList();

			return View(message);
		}
	}
}
