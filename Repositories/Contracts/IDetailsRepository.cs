using Entitiess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IDetailsRepository
    {
        IEnumerable<Details> GetAllDetails(bool trackChanges);
        IQueryable<Details> GetResult(int id, bool trackChanges);
        IQueryable <Details> GetOneDetailsById(int id, bool trackChanges);
        Details CreateOneDetailsWithTypeId(Details details, int TypeId);
        Details CreateOneDetails(Details details);
        void UpdateOneDetails(Details details);
        void DeleteOneDetails(Details details , bool trackChanges);
      
    }
}
