using FluentValidation;
using IdentityMessageProject.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMessageProject.BusinessLayer.ValidationRules
{
	public class NewMessageValidator : AbstractValidator<Message>
	{
		public NewMessageValidator()
		{
			RuleFor(x=>x.ReceiverId).NotEmpty().WithMessage("Alıcı seçiniz");
			RuleFor(x => x.Title).NotEmpty().WithMessage("Konu boş geçilemez");
			RuleFor(x => x.Title).MinimumLength(5).WithMessage("Konu için en az 5 karakter girilmeli");
			RuleFor(x => x.Content).NotEmpty().WithMessage("Mesaj alanı boş geçilemez");
			RuleFor(x => x.Content).MinimumLength(10).WithMessage("Mesaj alanı için en az 10 karakter girilmeli");
		}
	}
}
