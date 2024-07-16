using DocumentFormat.OpenXml.Bibliography;
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
    public class TownManager : ITownService
    {
        private readonly IRepositoryManager _manager; 

        public TownManager(IRepositoryManager repositoryManager)
        {
            _manager = repositoryManager;
        }

        public Town CreateOneTown(Town town)
        {
            if (town == null) 
                throw new ArgumentNullException(nameof(town));

            Entitiess.Models.City newCity = _manager.City.CreateOneCity(town.City);

            _manager.Town.CreateOneTown(town);
            _manager.Save();
            return town;
        }

        public Town DeleteOneTown(int id, bool trackChanges)
        {
            var entity = GetOneTownById(id, trackChanges);
            if(entity == null)
            {
                throw new Exception($"Town with id:{id} could not found");
            }
            var cityToDelete = entity.City;
            if(cityToDelete != null)
            {
                _manager.City.DeleteOneCity(cityToDelete, trackChanges: true);
            }
            _manager.Town.DeleteOneTown(entity, trackChanges);
            _manager.Save();
            return entity;
        }

        public List<Town> GetAllTown(bool trackChanges)
        {
            var town = _manager.Town.GetAllTown(trackChanges).ToList();
            if(!town.Any())
            {
                return null;
            }
            foreach (var _town in town)
            {
                _town.City = _manager.City.GetOneCityById(_town.CityId, false).FirstOrDefault();
            }
            return town;
        }

        public Town GetOneTownById(int id, bool trackChanges)
        {
            var town = _manager.Town.GetOneTownById(id, trackChanges).FirstOrDefault();
            if(town == null) 
            {
                return null;
            }
            town.City = _manager.City.GetOneCityById(town.CityId, false).FirstOrDefault();
            return town;
        }

        public void UpdateTown(bool trackChanges, int id, Town updatedTown)
        {
            var town = _manager.Town.GetAllTown(trackChanges).ToList();
            if(!town.Any())
            {
                throw new Exception("There are no Town to update.");
            }
            foreach(var _town in town)
            {
                var city = _manager.City.GetOneCityById(_town.CityId, trackChanges).FirstOrDefault();
                if (city == null)
                    continue;

                if(_town.TownId == updatedTown.TownId)
                {
                    _town.TownId = updatedTown.TownId;
                    city.CityId = updatedTown.CityId;
                    _town.TownName = updatedTown.TownName;

                    _manager.Town.UpdateOneTown(_town);
                    _manager.City.UpdateOneCity(city);
                }
            }
        }
        public List<Town> GetOneTCityById(int CityId)
        {
            var townList = _manager.Town.GetOneTCityById(CityId);
            return townList;
        }
    }
}
