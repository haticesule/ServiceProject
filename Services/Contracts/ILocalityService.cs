using Entitiess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ILocalityService
    {
        List<Locality> GetAllLocality(bool trackChanges);
        List<Locality> GetLocalityByTownId(int TownId);
        Locality GetOneLocalityById(int id, bool trackChanges);
        Locality DeleteOneLocality(int id, bool trackChanges);
        Locality CreateOneLocality(Locality locality);
        void UpdateLocality(bool trackChanges, int id, Locality updatedLocality);
    }
}
