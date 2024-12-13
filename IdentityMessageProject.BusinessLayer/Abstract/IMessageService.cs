using IdentityMessageProject.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMessageProject.BusinessLayer.Abstract
{
	public interface IMessageService : IGenericService<Message>
	{
		public Message TGetMessageWithAppUser(int id);
		void TChangeIsReadStatus(int id);
		void TChangeIsDeleteStatus(int id);
		void TChangeIsDraftStatus(int id);
	}
}
