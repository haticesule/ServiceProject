using Entitiess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IDetailsService
    {
        List<Details> GetAllDetails(bool trackChanges);
        Details GetOneDetailsById(int id, bool trackChanges);
        Details DeleteOneDetails(int id, bool trackChanges);
        Details CreateOneDetails(Details details);
        Details CreateOneDetailsWithTypeId(Details details, int TypeId);
        void UpdateOneDetails(int id, Details details, bool trackChanges);
    }
}
