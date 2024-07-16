using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office.PowerPoint.Y2021.M06.Main;
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
    public class LocalityRepository : ILocalityRepository
    {
        private readonly IRepositoryBase<Locality> localityRepositoryBase;
        private readonly RepositoryContext _context;

        public LocalityRepository(RepositoryContext context, IRepositoryBase<Locality> localityRepository)
        {
            _context = context;
            localityRepositoryBase = localityRepository;
        }
        Locality ILocalityRepository.CreateOneLocality(Locality locality)
        {
            return this.localityRepositoryBase.Create(locality);
        }
        public void DeleteOneLocality(Locality locality, bool trackChanges)
        {
            this.localityRepositoryBase.Delete(locality);
        }

        public IEnumerable<Locality> GetAllLocality(bool trackChanges)
        {
            return this.localityRepositoryBase.FindAll(trackChanges);
        }

        public IQueryable<Locality> GetOneLocalityById(int id, bool trackChanges)
        {
            var expression = trackChanges ? (Expression<Func<Locality, bool>>)(c => c.LocalityId == id) : (c => c.LocalityId == id);
            return (IQueryable<Locality>)this.localityRepositoryBase.FindByCondition(expression, trackChanges);
        }

        public void UpdateOneLocality(Locality locality)
        {
            this.localityRepositoryBase.Update(locality);
        }

        public List<Locality> GetLocalityByTownId(int TownId)
        {
            var locality = _context.Locality.Where(t => t.TownId == TownId).ToList();
            return locality;
        }
    }
}

