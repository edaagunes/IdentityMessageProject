﻿using IdentityMessageProject.BusinessLayer.Abstract;
using IdentityMessageProject.DataAccessLayer.Abstract;
using IdentityMessageProject.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMessageProject.BusinessLayer.Concrete
{
	public class MessageManager : IMessageService
	{
		private readonly IMessageDal _messageDal;

		public MessageManager(IMessageDal messageDal)
		{
			_messageDal = messageDal;
		}

		public void TChangeIsDeleteStatus(int id)
		{
			_messageDal.ChangeIsDeleteStatus(id);
		}

		public void TChangeIsDraftStatus(int id)
		{
			_messageDal.ChangeIsDraftStatus(id);
		}

		public void TChangeIsReadStatus(int id)
		{
			_messageDal.ChangeIsReadStatus(id);
		}

		public void TDelete(int id)
		{
			_messageDal.Delete(id);
		}

		public List<Message> TGetAll()
		{
			return _messageDal.GetAll();
		}

		public Message TGetById(int id)
		{
			return _messageDal.GetById(id);
		}

		public Message TGetMessageWithAppUser(int id)
		{
			return _messageDal.GetMessageWithAppUser(id);
		}

		public void TInsert(Message entity)
		{
			_messageDal.Insert(entity);
		}

		public void TUpdate(Message entity)
		{
			_messageDal.Update(entity);
		}
	}
}
