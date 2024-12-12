using IdentityMessageProject.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMessageProject.DataAccessLayer.Abstract
{
	public interface IMessageDal : IGenericDal<Message>
	{
		Message GetMessageWithAppUser(int id);
		void ChangeIsReadStatus(int id);
		 void ChangeIsDeleteStatus(int id);
	}
}
