using IdentityMessageProject.DataAccessLayer.Abstract;
using IdentityMessageProject.DataAccessLayer.Context;
using IdentityMessageProject.DataAccessLayer.Repositories;
using IdentityMessageProject.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMessageProject.DataAccessLayer.EntityFramework
{
	public class EfAppUserDal : GenericRepository<AppUser>, IAppUserDal
	{
		public EfAppUserDal(MessageContext context) : base(context)
		{
		}
	}
}
