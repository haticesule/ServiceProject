using Entitiess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IDriverService
    {
        List <Driver> GetAllDriver(bool trackChanges);
        Driver GetOneDriverById(int id, bool trackChanges);
        Driver DeleteOneDriver(int id, bool trackChanges);
        Driver CreateOneDriver(Driver driver);
        void UpdateDriver(bool trackChanges, Driver updatedDrivers);
        (bool, string) AuthenticateDriver(LoginRequest loginRequest);
    }
}
