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
		public bool IsRead { get; set; }
		public bool IsDelete { get; set; }
		public bool IsDraft { get; set; }

		public int? SenderId { get; set; }
		public AppUser Sender { get; set; }

		public int? ReceiverId { get; set; }
		public AppUser Receiver { get; set; }
	}
}
