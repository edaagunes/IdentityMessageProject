using FluentValidation.AspNetCore;
using IdentityMessageProject.BusinessLayer.Abstract;
using IdentityMessageProject.BusinessLayer.Concrete;
using IdentityMessageProject.DataAccessLayer.Abstract;
using IdentityMessageProject.DataAccessLayer.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMessageProject.BusinessLayer.Container
{
	public static class Extensions
	{
		public static void ContainerDependencies(this IServiceCollection services)
		{
			services.AddMvc().AddFluentValidation();

			services.AddScoped<IAppUserDal, EfAppUserDal>();
			services.AddScoped<IAppUserService, AppUserManager>();

			services.AddScoped<IMessageDal, EfMessageDal>();
			services.AddScoped<IMessageService, MessageManager>();
		}
	}
}
