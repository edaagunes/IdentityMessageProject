using IdentityMessageProject.EntityLayer.Concrete;
using IdentityMessageProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityMessageProject.Controllers
{
	public class RegisterController : Controller
	{
		private readonly UserManager<AppUser> _userManager;

		public RegisterController(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Index(RegisterViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model); 
			}

			AppUser appUser =new AppUser()
			{
				Name = model.Name,
				Surname = model.Surname,
				Email = model.Email,
				UserName=model.Username,
				ImageUrl="test"
			};

			var result=await _userManager.CreateAsync(appUser,model.Password);

			if (result.Succeeded)
			{
				return RedirectToAction("Index","Login");
			}

			else
			{
				foreach (var item in result.Errors)
				{
					ModelState.AddModelError("", item.Description);
				}
			}

			return View();
		}
	}
}
