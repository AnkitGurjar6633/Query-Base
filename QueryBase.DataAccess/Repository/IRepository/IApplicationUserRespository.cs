using QueryBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBase.DataAccess.Repository.IRepository
{
    public interface IApplicationUserRespository : IRepository<ApplicationUser>
    {
        void Remove(ApplicationUser user);
    }
}
