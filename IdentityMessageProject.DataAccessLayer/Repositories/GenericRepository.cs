using IdentityMessageProject.DataAccessLayer.Abstract;
using IdentityMessageProject.DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMessageProject.DataAccessLayer.Repositories
{
	public class GenericRepository<T> : IGenericDal<T> where T : class
	{
		private readonly MessageContext _context;

		public GenericRepository(MessageContext context)
		{
			_context = context;
		}

		public void Insert(T entity)
		{
			_context.Set<T>().Add(entity);
			_context.SaveChanges();
		}

		public void Delete(int id)
		{
			var values = _context.Set<T>().Find(id);
			_context.Set<T>().Remove(values);
			_context.SaveChanges();
		}

		public List<T> GetAll()
		{
			return _context.Set<T>().ToList();
		}

		public T GetById(int id)
		{
			return _context.Set<T>().Find(id);
		}

		public void Update(T entity)
		{
			_context.Set<T>().Update(entity);
			_context.SaveChanges();
		}

	}
}
