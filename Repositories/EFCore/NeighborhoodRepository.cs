using Entitiess.Models;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class NeighborhoodRepository : INeighborhoodRepository
    {
        private readonly IRepositoryBase<Neighborhood> neighborhoodRepositoryBase;
        private readonly RepositoryContext _context;

        public NeighborhoodRepository(RepositoryContext context, IRepositoryBase<Neighborhood> neighborhoodRepository)
        {
            _context = context;
            neighborhoodRepositoryBase = neighborhoodRepository;
        }

        public Neighborhood CreateOneNeighborhood(Neighborhood neighborhood)
        {
            return neighborhoodRepositoryBase.Create(neighborhood);
        }

        public void DeleteOneNeighborhood(Neighborhood neighborhood, bool trackChanges)
        {
            this.neighborhoodRepositoryBase.Delete(neighborhood);
        }

        public IEnumerable<Neighborhood> GetAllNeighborhood(bool trackChanges)
        {
            return this.neighborhoodRepositoryBase.FindAll(trackChanges);
        }

        public List<Neighborhood> GetNeighborhoodByLocalityId(int LocalityId)
        {
            var neighborhood = _context.Neighborhood.Where(t => t.LocalityId == LocalityId).ToList();
            return neighborhood;
        }

        public IQueryable<Neighborhood> GetOneNeighborhoodById(int id, bool trackChanges)
        {
            var expression = trackChanges ? (Expression<Func<Neighborhood, bool>>)(c => c.NeighborhoodId == id) : (c => c.NeighborhoodId == id);
            return (IQueryable<Neighborhood>)this.neighborhoodRepositoryBase.FindByCondition(expression, trackChanges);
        }
        public void UpdateOneNeighborhood(Neighborhood locality)
        {
            this.neighborhoodRepositoryBase.Update(locality);
        }
    }
}
