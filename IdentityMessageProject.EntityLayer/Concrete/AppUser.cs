using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMessageProject.EntityLayer.Concrete
{
	public class AppUser : IdentityUser<int>
	{
		public string Name { get; set; }
		public string Surname{ get; set; }
		public string ImageUrl{ get; set; }
		public List<Message> Messages { get; set; }
	}
}
