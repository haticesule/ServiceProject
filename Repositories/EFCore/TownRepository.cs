using DocumentFormat.OpenXml.Bibliography;
using Entitiess.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class TownRepository : ITownRepository
    {
        private readonly IRepositoryBase<Town> townRepositoryBase;
        private readonly RepositoryContext _context;

        public TownRepository(RepositoryContext context, IRepositoryBase<Town> townRepository)
        {
            _context = context;
            townRepositoryBase = townRepository;
        }
        Town CreateOneTown(Town town)
        {
            return this.townRepositoryBase.Create(town);
        }

        public void DeleteOneTown(Town town, bool trackChanges)
        {
            this.townRepositoryBase.Delete(town);
        }

        public IEnumerable<Town> GetAllTown(bool trackChanges)
        {
            return this.townRepositoryBase.FindAll(trackChanges);
        }

        public IQueryable<Town> GetOneTownById(int id, bool trackChanges)
        {
            var expression = trackChanges ? (Expression<Func<Town, bool>>)(c => c.TownId == id) : (c => c.TownId == id);
            return (IQueryable<Town>)this.townRepositoryBase.FindByCondition(expression, trackChanges);
        }

        public void UpdateOneTown(Town town)
        {
            this.townRepositoryBase.Update(town);
        }

        Town ITownRepository.CreateOneTown(Town town)
        {
            return this.townRepositoryBase.Create(town);
        }

        public List<Town> GetOneTCityById(int CityId)
        {
            var towns = _context.Town.Where(t => t.CityId == CityId).ToList();
            return towns;
        }
    }
}
