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
    public class DetailManager : IDetailsService
    {
        private readonly IRepositoryManager _manager;
        public DetailManager(IRepositoryManager repositorymanager)
        {
            _manager = repositorymanager;
        }

        public Details CreateOneDetails(Details details)
        {
            if(details == null)
                throw new ArgumentNullException(nameof(details));

            _manager.Details.CreateOneDetails(details);
            _manager.Save();
            return details;
        }

        public Details CreateOneDetailsWithTypeId(Details details, int TypeId)
        {
            if (details == null)
                throw new ArgumentNullException(nameof(details));

            _manager.Details.CreateOneDetails(details);
            
            Admin newAdminObject = new Admin()
            {
                TypeId = TypeId,
                DetailsId = details.DetailsId,

            };
            Admin newAdmin = _manager.Admin.CreateOneAdmin(newAdminObject);
            return details;
        }

        public Details DeleteOneDetails(int id, bool trackChanges)
        {
            var entity = GetOneDetailsById(id, trackChanges);
            if (entity == null)
            { 
                throw new Exception($"Details with id:{id} could not found.");
            }

            var adminToDelete = entity.Admin;
            if (adminToDelete != null)
            {
               _manager.Admin.DeleteOneAdmin(adminToDelete, trackChanges: true);
            }

            _manager.Details.DeleteOneDetails(entity, false);
            _manager.Save();
            return entity;
        }

        public List<Details> GetAllDetails(bool trackChanges)
        {
            var details = _manager.Details.GetAllDetails(trackChanges).ToList();
            if(!details.Any())
            {
                return null;
            }
            foreach (var detail in details)
            {
                detail.Admin = _manager.Admin.GetOneAdminByDetailId(detail.DetailsId, false).FirstOrDefault();
            }
            return details;
        }


        public Details GetOneDetailsById(int id, bool trackChanges)
        {
            var details =  _manager.Details.GetOneDetailsById(id, trackChanges).FirstOrDefault();
            if(details == null)
            {
                return null;
            }
            details.Admin = _manager.Admin.GetOneAdminByDetailId(details.DetailsId, false).FirstOrDefault();
            return details;
        }

        public void UpdateOneDetails(int id, Details details, bool trackChanges)
        {
            var entity = _manager.Details.GetOneDetailsById(id, trackChanges);
            if (entity == null)
                throw new Exception($"Details with id:{id} could not be found.");

            Details detailsEntity = entity as Details;

            if (detailsEntity != null)
            {
                detailsEntity.DetailsId = details.DetailsId;

                _manager.Details.UpdateOneDetails(detailsEntity);
                _manager.Save();
            }
            else
            {
                throw new Exception($"Details with id:{id} could not be found.");
            }
        }
    }
}
