using IdentityMessageProject.EntityLayer.Concrete;
using IdentityMessageProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityMessageProject.Controllers
{
	public class LoginController : Controller
	{
		private readonly SignInManager<AppUser> _signInManager;

		public LoginController(SignInManager<AppUser> signInManager)
		{
			_signInManager = signInManager;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Index(LoginViewModel model)
		{
			var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, true);
			if (result.Succeeded)
			{
				return RedirectToAction("Index", "Profile");
			}
			else
			{
				return View();
			}
		}

		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync(); 
			return RedirectToAction("Index", "Login"); 
		}
	}
}
