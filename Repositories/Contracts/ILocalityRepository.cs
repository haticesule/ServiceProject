using Entitiess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface ILocalityRepository
    {
        IEnumerable<Locality> GetAllLocality(bool trackChanges);
        IQueryable<Locality> GetOneLocalityById(int id, bool trackChanges);
        Locality CreateOneLocality(Locality locality);
        void UpdateOneLocality(Locality locality);
        void DeleteOneLocality(Locality locality, bool trackChanges);
        List<Locality> GetLocalityByTownId(int TownId);
    }
}
