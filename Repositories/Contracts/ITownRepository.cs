using Entitiess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface ITownRepository
    {
        IEnumerable<Town> GetAllTown(bool trackChanges);
        IQueryable<Town> GetOneTownById(int id, bool trackChanges);
        List <Town> GetOneTCityById(int CityId);
        Town CreateOneTown(Town town);
        void UpdateOneTown(Town town);
        void DeleteOneTown(Town town, bool trackChanges);
    }
}
