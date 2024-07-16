using Entitiess.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LocalityManager : ILocalityService
    {
        private readonly IRepositoryManager _manager;

        public LocalityManager(IRepositoryManager repositoryManager)
        {
            _manager = repositoryManager;
        }

        public Locality CreateOneLocality(Locality locality)
        {
            if(locality == null)
                throw new ArgumentNullException(nameof(locality));

            Town newTown = _manager.Town.CreateOneTown(locality.Town);

            _manager.Locality.CreateOneLocality(locality);
            _manager.Save();
            return locality;
        }

        public Locality DeleteOneLocality(int id, bool trackChanges)
        {
            var entity = GetOneLocalityById(id, trackChanges);
            if (entity == null)
            {
                throw new Exception($"Locality with id:{id} could not found");
            }
            var townToDelete = entity.Town;
            if (townToDelete != null)
            {
                _manager.Town.DeleteOneTown(townToDelete, trackChanges: true);
            }
            _manager.Locality.DeleteOneLocality(entity, trackChanges);
            _manager.Save();
            return entity;
        }

        public List<Locality> GetAllLocality(bool trackChanges)
        {
            var locality = _manager.Locality.GetAllLocality(trackChanges).ToList();
            if (!locality.Any())
            {
                return null;
            }
            foreach (var _locality in locality)
            {
                _locality.Town = _manager.Town.GetOneTownById(_locality.TownId, false).FirstOrDefault();
            }
            return locality;
        }

        public List<Locality> GetLocalityByTownId(int TownId)
        {
            var localityList = _manager.Locality.GetLocalityByTownId(TownId);
            return localityList;
        }

        public Locality GetOneLocalityById(int id, bool trackChanges)
        {
            var locality = _manager.Locality.GetOneLocalityById(id, trackChanges).FirstOrDefault();
            if (locality == null)
            {
                return null;
            }
            locality.Town = _manager.Town.GetOneTownById(locality.TownId, false).FirstOrDefault();
            return locality;
        }

        public void UpdateLocality(bool trackChanges, int id, Locality updatedLocality)
        {
            var locality = _manager.Locality.GetAllLocality(trackChanges).ToList();
            if (!locality.Any())
            {
                throw new Exception("There are no Locality to update.");
            }
            foreach (var _locality in locality)
            {
                var town = _manager.Town.GetOneTownById(_locality.TownId, trackChanges).FirstOrDefault();
                if (town == null)
                    continue;

                if (_locality.TownId == updatedLocality.TownId)
                {
                    _locality.LocalityId = updatedLocality.LocalityId;
                    town.TownId = updatedLocality.TownId;
                    _locality.LocalityName = updatedLocality.LocalityName;

                    _manager.Locality.UpdateOneLocality(_locality);
                    _manager.Town.UpdateOneTown(town);
                }
            }
        }
    }
}
