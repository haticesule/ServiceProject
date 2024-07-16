using Entitiess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface INeighborhoodRepository
    {
        IEnumerable<Neighborhood> GetAllNeighborhood(bool trackChanges);
        IQueryable<Neighborhood> GetOneNeighborhoodById(int id, bool trackChanges);
        List<Neighborhood> GetNeighborhoodByLocalityId(int LocalityId);
        Neighborhood CreateOneNeighborhood(Neighborhood neighborhood);
        void UpdateOneNeighborhood(Neighborhood neighborhood);
        void DeleteOneNeighborhood(Neighborhood neighborhood, bool trackChanges);
    }
}
