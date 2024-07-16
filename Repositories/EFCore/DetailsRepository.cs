using DocumentFormat.OpenXml.ExtendedProperties;
using Entitiess.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class DetailsRepository : IDetailsRepository
    {
        private readonly IRepositoryBase<Details> detailsRepositoryBase;
        private readonly IRepositoryBase<Admin> adminRepositoryBase;
       

        private readonly RepositoryContext _context;
        

        public DetailsRepository(RepositoryContext context, IRepositoryBase<Details> detailsRepository, IRepositoryBase<Admin> adminRepository)
        {
            _context = context;
            detailsRepositoryBase = detailsRepository;
            adminRepositoryBase = adminRepository; 
            
        }

        public Details CreateOneDetailsWithTypeId(Details details, int TypeId)
        {
            if (details == null)
                throw new ArgumentNullException(nameof(details));

            Details newDetails = CreateOneDetails(details);
          
            Admin newAdminObject = new Admin()
            {
                TypeId = TypeId,
                DetailsId = newDetails.DetailsId,
                Password = newDetails.Admin.Password,

            };
            Admin newAdmin = adminRepositoryBase.Create(newAdminObject);
            return details;
        }

        public Details CreateOneDetails(Details details)
        {
            return this.detailsRepositoryBase.Create(details);
        }

        public void DeleteOneDetails(Details details, bool trackChanges)
        {
            detailsRepositoryBase.Delete(details);
        }

        public IEnumerable<Details> GetAllDetails(bool trackChanges)
        {
            return this.detailsRepositoryBase.FindAll(trackChanges);
        }

        public IQueryable<Details> GetOneDetailsById(int id, bool trackChanges)
        {
            var expression = trackChanges ? (Expression<Func<Details, bool>>)(c => c.DetailsId == id) : (c => c.DetailsId == id);
            return (IQueryable<Details>)this.detailsRepositoryBase.FindByCondition(expression, trackChanges);
        }

        public IQueryable<Details> GetResult(int id, bool trackChanges)
        {
            IQueryable<Details> query = (IQueryable<Details>)_context.Results;

            if(!trackChanges)
            {
                query = query.AsNoTracking();
            }
            query = query.Where(r => r.DetailsId == id);
            return query;
        }

        public void UpdateOneDetails(Details details)
        {
            this.detailsRepositoryBase.Update(details);
        }
    }
}
