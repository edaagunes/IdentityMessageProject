using IdentityMessageProject.DataAccessLayer.Abstract;
using IdentityMessageProject.DataAccessLayer.Context;
using IdentityMessageProject.DataAccessLayer.Repositories;
using IdentityMessageProject.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMessageProject.DataAccessLayer.EntityFramework
{
	public class EfMessageDal : GenericRepository<Message>, IMessageDal
	{
		public EfMessageDal(MessageContext context) : base(context)
		{
		}

		public void ChangeIsReadStatus(int id)
		{
			using (var context = new MessageContext())
			{
				var values = context.Messages.FirstOrDefault(x => x.MessageId == id);
				if (values != null && values.IsRead == false)
				{
					values.IsRead = true;
					context.Messages.Update(values);
					context.SaveChanges();
				}
			}
		}

		public Message GetMessageWithAppUser(int id)
		{
			using (var context = new MessageContext())
			{
				return context.Messages.Include(x => x.Sender).FirstOrDefault(x => x.MessageId == id);
			}
		}

		public void ChangeIsDeleteStatus(int id)
		{
			using (var context = new MessageContext())
			{
				var values = context.Messages.FirstOrDefault(x => x.MessageId == id);
				if (values != null && values.IsDelete == false)
				{
					values.IsDelete = true;
					context.Messages.Update(values);
					context.SaveChanges();
				}
			}
		}

		public void ChangeIsDraftStatus(int id)
		{
			using (var context = new MessageContext())
			{
				var message = context.Messages.FirstOrDefault(x => x.MessageId == id);
				if (message != null)
				{
					message.IsDraft = true;
					context.Messages.Update(message);
					context.SaveChanges();
				}
			}
		}

	}
}
