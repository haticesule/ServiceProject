using Entitiess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IAdminRepository
    {
        IEnumerable<Admin> GetAllAdmin(bool trackChanges);
        IQueryable<Admin> GetOneAdminById(int id, bool trackChanges);
        IQueryable<Admin> GetOneAdminByDetailId(int id, bool trackChanges);
        Admin CreateOneAdmin(Admin admin);
        void UpdateOneAdmin(Admin admin);
        bool DeleteOneAdmin(Admin admin, bool trackChanges);

    }
}
