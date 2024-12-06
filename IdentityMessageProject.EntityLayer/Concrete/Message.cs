﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMessageProject.EntityLayer.Concrete
{
	public class Message
	{
		public int MessageId { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime CreatedDate { get; set; }
		public bool Status { get; set; }

		public int AppUserId { get; set; }
		public AppUser AppUser { get; set; }
	}
}
