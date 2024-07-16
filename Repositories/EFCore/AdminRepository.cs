using DocumentFormat.OpenXml.Spreadsheet;
using Entitiess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class AdminRepository : IAdminRepository
    {
        private readonly IRepositoryBase<Admin> adminRepositoryBase;
        private readonly RepositoryContext _context;

        public AdminRepository(RepositoryContext context, IRepositoryBase<Admin> adminRepository)
        {
            _context = context;
            adminRepositoryBase = adminRepository;
        }
        public Admin AuthenticateAdmin(LoginRequest loginRequest)
        {
            throw new NotImplementedException();
        }

        public Admin CreateOneAdmin(Admin admin)
        {
            this.adminRepositoryBase.Create(admin);
            return admin;
        }

        public bool DeleteOneAdmin(Admin admin, bool trackChanges)
        {
            if (admin == null)
            {
                throw new ArgumentNullException(nameof(admin));
            }
            try
            {
                return true;
            }
            catch (DbUpdateException ex)
            {
                return false;
            }

        }

        public IEnumerable<Admin> GetAllAdmin(bool trackChanges)
        {
            return this.adminRepositoryBase.FindAll(trackChanges);
        }

        public IQueryable<Admin> GetOneAdminByDetailId(int id, bool trackChanges)
        {
            var expression = trackChanges ? (Expression<Func<Admin, bool>>)(c => c.DetailsId == id) : (c => c.DetailsId == id);
            return (IQueryable<Admin>)this.adminRepositoryBase.FindByCondition(expression, trackChanges);
        }

        public IQueryable<Admin> GetOneAdminById(int id, bool trackChanges)
        {
            var expression = trackChanges ? (Expression<Func<Admin, bool>>)(c => c.AdminId == id) : (c => c.AdminId == id);
            return (IQueryable<Admin>)this.adminRepositoryBase.FindByCondition(expression, trackChanges);
        }

        public void UpdateOneAdmin(Admin admin)
        {
            this.adminRepositoryBase.Update(admin);
        }
    }
}
