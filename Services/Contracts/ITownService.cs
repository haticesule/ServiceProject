using Entitiess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ITownService
    {
        List<Town> GetAllTown(bool trackChanges);
        
        List<Town> GetOneTCityById(int CityId);
        Town GetOneTownById(int id, bool trackChanges);
        Town DeleteOneTown(int id, bool trackChanges);
        Town CreateOneTown(Town town);
        void UpdateTown(bool trackChanges, int id, Town updatedTown);
    }
}
