using DocumentFormat.OpenXml.Office2010.Excel;
using Entitiess.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CityManager : ICityService
    {
        private readonly IRepositoryManager _manager;

        public CityManager(IRepositoryManager repositoryManager)
        {
            _manager = repositoryManager;
        }

        public City CreateOneCity(City city)
        {
            if (city == null)
                throw new ArgumentNullException(nameof(city));

            _manager.City.CreateOneCity(city);
            _manager.Save();
            return city;
        }

        public City DeleteOneCity(int id, bool trackChanges)
        {
            var entity = _manager.City.GetOneCityById(id, trackChanges).SingleOrDefault();
            if (entity == null)
                throw new Exception($"City with id:{id} could not found.");

            _manager.City.DeleteOneCity(entity, false);
            _manager.Save();
            return entity;
        }

        public List<City> GetAllCity(bool trackChanges)
        {
            var city = _manager.City.GetAllCity(trackChanges).ToList();
            return city;
        }

        public City GetOneCityById(int id, bool trackChanges)
        {
            return _manager.City.GetOneCityById(id, trackChanges).FirstOrDefault();
        }

        public void UpdateCity(bool trackChanges,int id, City updatedCity)
        {
            var entity = _manager.City.GetOneCityById(id, trackChanges);
            if (entity == null)
                throw new Exception($"City with id:{id} could not be found.");

            City cityEntity = entity as City;

            if (cityEntity != null)
            {
                cityEntity.CityId = updatedCity.CityId;

                _manager.City.UpdateOneCity(cityEntity);
                _manager.Save();
            }
            else
            {
                throw new Exception($"City with id:{id} could not be found.");
            }
        }
    }
}
