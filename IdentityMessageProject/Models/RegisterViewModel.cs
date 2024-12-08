using System.ComponentModel.DataAnnotations;

namespace IdentityMessageProject.Models
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage = "Lütfen Adınızı Giriniz")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Lütfen Soyadınız Giriniz")]
		public string Surname { get; set; }
		[Required(ErrorMessage = "Lütfen Email Adresinizi Giriniz")]
		[EmailAddress(ErrorMessage = "Geçerli bir Email adresi giriniz.")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Lütfen Kullanıcı Adınızı Giriniz")]
		public string Username { get; set; }
		[Required(ErrorMessage = "Lütfen Şifrenizi Giriniz")]
		public string Password { get; set; }
	}
}
