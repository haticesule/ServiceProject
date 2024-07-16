using DocumentFormat.OpenXml.Drawing.Spreadsheet;
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
    public class NeighborhoodManager : INeighborhoodService
    {
        private readonly IRepositoryManager _manager;

        public  NeighborhoodManager(IRepositoryManager repositoryManager)
        {
            _manager = repositoryManager;
        }
        public Neighborhood CreateOneNeighborhood(Neighborhood neighborhood)
        {
            if(neighborhood == null) 
                throw new ArgumentNullException(nameof(neighborhood));

            Locality newLocality = _manager.Locality.CreateOneLocality(neighborhood.Locality);
            _manager.Neighborhood.CreateOneNeighborhood(neighborhood);
            _manager.Save();
            return neighborhood;
        }

        public Neighborhood DeleteOneNeighborhood(int id, bool trackChanges)
        {
            var entity = GetOneNeighborhoodById(id, trackChanges);
            if(entity == null)
            {
                throw new Exception($"Neighborhood with id:{id} could not found");
            }
            var LocalityToDelete = entity.Locality;
            if( LocalityToDelete != null )
            {
                _manager.Locality.DeleteOneLocality(LocalityToDelete, trackChanges: true);
            }
            _manager.Neighborhood.DeleteOneNeighborhood(entity, trackChanges);
            _manager.Save();
            return entity;
        }

        public List<Neighborhood> GetAllNeighborhood(bool trackChanges)
        {
            var neighborhood = _manager.Neighborhood.GetAllNeighborhood(trackChanges).ToList();
            if(!neighborhood.Any())
            {
                return null;
            }
            foreach(var _neighborhood in neighborhood)
            {
                _neighborhood.Locality = _manager.Locality.GetOneLocalityById(_neighborhood.LocalityId,false).FirstOrDefault();
            }
            return neighborhood;
        }

        public List<Neighborhood> GetNeighborhoodByLocalityId(int LocalityId)
        {
            var neighborhoodList = _manager.Neighborhood.GetNeighborhoodByLocalityId(LocalityId);
            return neighborhoodList;
        }

        public Neighborhood GetOneNeighborhoodById(int id, bool trackChanges)
        {
            var neighborhood =  _manager.Neighborhood.GetOneNeighborhoodById(id, trackChanges).FirstOrDefault();
            if( neighborhood == null )
            {
                return null;
            }
            neighborhood.Locality = _manager.Locality.GetOneLocalityById(neighborhood.LocalityId, trackChanges).FirstOrDefault();
            return neighborhood;
        }

        public void UpdateNeighborhood(bool trackChanges, int id, Neighborhood updatedNeighborhood)
        {
            var neighborhood = _manager.Neighborhood.GetAllNeighborhood(trackChanges).ToList();
            if( !neighborhood.Any() )
            {
                throw new Exception("There are no neighborhood to update");
            }
            foreach(var _neighborhood in neighborhood)
            {
                var locality = _manager.Locality.GetOneLocalityById(_neighborhood.LocalityId, trackChanges).FirstOrDefault();
                if (locality == null)
                    continue;
                if(_neighborhood.LocalityId == updatedNeighborhood.LocalityId)
                {
                    _neighborhood.NeighborhoodId = updatedNeighborhood.NeighborhoodId;
                    locality.LocalityId = updatedNeighborhood.LocalityId;
                    _neighborhood.NeighborhoodName = updatedNeighborhood.NeighborhoodName;

                    _manager.Neighborhood.UpdateOneNeighborhood(_neighborhood);
                    _manager.Locality.UpdateOneLocality(locality);
                }
            }
        }
    }
}
