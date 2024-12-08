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

		public Message GetMessageWithAppUser(int id)
		{
			using (var context = new MessageContext())
			{
				return context.Messages.Include(x => x.Sender).FirstOrDefault(x => x.MessageId == id);
			}
		}
	}
}
