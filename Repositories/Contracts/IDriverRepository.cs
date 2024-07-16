using Entitiess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IDriverRepository
    {
        IEnumerable<Driver> GetAllDriver(bool trackChanges);
        IQueryable<Driver> GetResult(int id, bool trackChain);
        IQueryable<Driver> GetOneDriverById(int id, bool trackChanges);
        void CreateOneDriver(Driver driverDetail);
        void UpdateOneDriver(Driver driverDetail);
        void DeleteOneDriver(Driver driverDetail);
    }
}
