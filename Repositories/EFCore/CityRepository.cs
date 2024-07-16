using DocumentFormat.OpenXml.Office.PowerPoint.Y2021.M06.Main;
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
    public class CityRepository : ICityRepository
    {
        private readonly IRepositoryBase<City> cityRepositoryBase;
        private readonly RepositoryContext _context;

        public CityRepository(RepositoryContext context, IRepositoryBase<City> cityRepository)
        {
            _context = context;
            cityRepositoryBase = cityRepository;
        }

        public City CreateOneCity(City city)
        {
            return this.cityRepositoryBase.Create(city);
        }

        public void DeleteOneCity(City city, bool trackChanges)
        {
            this.cityRepositoryBase.Delete(city);   
        }

        public IEnumerable<City> GetAllCity(bool trackChanges)
        {
            return this.cityRepositoryBase.FindAll(trackChanges);
        }

        public IQueryable<City> GetOneCityById(int id, bool trackChanges)
        {
            var expression = trackChanges ? (Expression<Func<City, bool>>)(c => c.CityId == id) : (c => c.CityId == id);
            return (IQueryable<City>)this.cityRepositoryBase.FindByCondition(expression, trackChanges);
        }

        public void UpdateOneCity(City city)
        {
            this.cityRepositoryBase.Update(city);
        }
    }
}
