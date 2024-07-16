using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Entitiess.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class DriverRepository : IDriverRepository
    {

        private readonly IRepositoryBase<Driver> driverRepositoryBase;

        private readonly RepositoryContext _context;

        public DriverRepository(RepositoryContext context, IRepositoryBase<Driver> driverRepository)
        {
            _context = context;
            this.driverRepositoryBase = driverRepository;
        }

        public void CreateOneDriver(Driver drivers)
        {
            this.driverRepositoryBase.Create(drivers);
        }

        public void DeleteOneDriver(Driver drivers)
        {
            this.driverRepositoryBase.Delete(drivers);
        }

        public IEnumerable<Driver> GetAllDriver(bool trackChanges)
        {
            return this.driverRepositoryBase.FindAll(trackChanges);
        }
      
        public IQueryable<Driver> GetResult(int id, bool trackChanges)
        {
            IQueryable<Driver> query = (IQueryable<Driver>)_context.Results;

            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }

            query = query.Where(r => r.DriverId == id);
            return query;

        }
        public void UpdateOneDriver(Driver driverDetail)
        {
            this.driverRepositoryBase.Update(driverDetail);
        }


        IQueryable<Driver> IDriverRepository.GetOneDriverById(int id, bool trackChanges)
        {
            var expression = trackChanges ? (Expression<Func<Driver, bool>>)(c => c.DriverId == id) : (c => c.DriverId == id);
            return (IQueryable<Driver>)this.driverRepositoryBase.FindByCondition(expression, trackChanges);
        }
    }

}
