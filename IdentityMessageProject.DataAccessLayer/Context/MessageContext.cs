using IdentityMessageProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMessageProject.DataAccessLayer.Context
{
	public class MessageContext : IdentityDbContext<AppUser, AppRole, int>
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("server=DESKTOP-3OD251U\\SQLEXPRESS;initial catalog=MessageProjectDb;integrated security=true");
		}

		public DbSet<AppUser> AppUsers { get; set; }
		public DbSet<Message> Messages { get; set; }
	}
}
