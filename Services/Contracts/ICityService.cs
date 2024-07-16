using Entitiess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ICityService
    {
        List<City> GetAllCity(bool trackChanges);
        City GetOneCityById(int id, bool trackChanges);
        City DeleteOneCity(int id, bool trackChanges);
        City CreateOneCity(City city);
        void UpdateCity(bool trackChanges,int id, City updatedCity);
    }
}
