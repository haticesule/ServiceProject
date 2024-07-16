using Entitiess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface ICityRepository
    {
        IEnumerable<City> GetAllCity(bool trackChanges);
        IQueryable<City>GetOneCityById(int id, bool trackChanges);
        City CreateOneCity(City city);
        void UpdateOneCity(City city);
        void DeleteOneCity(City city, bool trackChanges);
    }
}
