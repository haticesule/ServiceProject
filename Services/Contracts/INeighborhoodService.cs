using Entitiess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface INeighborhoodService
    {
        List<Neighborhood> GetAllNeighborhood(bool trackChanges);
        List<Neighborhood> GetNeighborhoodByLocalityId(int LocalityId);
        Neighborhood GetOneNeighborhoodById(int id, bool trackChanges);
        Neighborhood DeleteOneNeighborhood(int id, bool trackChanges);
        Neighborhood CreateOneNeighborhood(Neighborhood neighborhood);
        void UpdateNeighborhood(bool trackChanges, int id, Neighborhood updatedNeighborhood);
    }
}
