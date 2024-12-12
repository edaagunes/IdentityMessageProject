using IdentityMessageProject.BusinessLayer.Abstract;
using IdentityMessageProject.EntityLayer.Concrete;
using IdentityMessageProject.Models.Profile;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityMessageProject.Controllers
{
	public class ProfileController : Controller
	{
		private readonly UserManager<AppUser> _userManager;

		public ProfileController(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var user = await _userManager.FindByNameAsync(User.Identity.Name);
			
			UpdateProfileViewModel viewModel = new UpdateProfileViewModel();
			viewModel.Name = user.Name;
			viewModel.Email = user.Email;
			viewModel.Surname = user.Surname;
			viewModel.Username = user.UserName;
			viewModel.ImageUrl = user.ImageUrl;

			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Index(UpdateProfileViewModel model)
		{
			var user = await _userManager.FindByNameAsync(User.Identity.Name);

			user.Name=model.Name;
			user.Surname=model.Surname;	
			user.Email=model.Email;
			user.UserName = model.Username;

			if (model.Image != null)
			{
				var resource = Directory.GetCurrentDirectory();
				var extension = Path.GetExtension(model.Image.FileName);
				var imageName = Guid.NewGuid() + extension;
				var saveLocation = Path.Combine(resource, "wwwroot/userImages", imageName);
				using (var stream = new FileStream(saveLocation, FileMode.Create))
				{
					await model.Image.CopyToAsync(stream);
				}
				user.ImageUrl = "/userImages/"+imageName;
			}

			else
			{
				user.ImageUrl = user.ImageUrl;
			}

			user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);

			var result = await _userManager.UpdateAsync(user);

			if (result.Succeeded)
			{
				return RedirectToAction("Index", "Login");
			}
			else
			{
				return View();
			}
			
		}
	}
}
