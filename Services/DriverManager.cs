using DocumentFormat.OpenXml.Bibliography;
using Entitiess.Models;
using Microsoft.IdentityModel.Tokens;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DriverManager : IDriverService
    {
        private readonly JwtService _jwtService;
        private readonly IRepositoryManager _manager;
        public DriverManager(IRepositoryManager repositoryManager, JwtService jwtService) 
        {
            _manager = repositoryManager;
            _jwtService = jwtService;
        }

        public (bool, string) AuthenticateDriver(LoginRequest loginRequest)
        {
            var driverList = GetAllDriver(false).ToList();
            var driver = driverList.FirstOrDefault(driver => driver.Details.Email == loginRequest.Email);

            if (driver == null)
            {
                return (false, "Bu email ile eşleşen bir sürücü mevcut değil.");
            }
            else
            {
                if (driver.Details.Admin.Password.Equals(loginRequest.Password))
                {
                    var details = driver.Details.Email;
                    return (true, _jwtService.GenerateJwt(driver.DriverId, details));
                }
                else
                {
                    return (false, "Şifre yanlış.");
                }
            }
        }

        public Driver CreateOneDriver(Driver drivers)
        {
            if (drivers == null)
                throw new ArgumentNullException(nameof(drivers));

            Details newDetails = _manager.Details.CreateOneDetailsWithTypeId(drivers.Details, 1);
            drivers.DetailsId = newDetails.DetailsId;
            _manager.Driver.CreateOneDriver(drivers);
            _manager.Save();
            return drivers;
        }
        public Driver DeleteOneDriver(int id, bool trackChanges)
        {
            var entity = GetOneDriverById(id, trackChanges);

            if (entity == null)
            {
                throw new Exception($"Driver with id:{id} could not be found.");
            }

            var detailsToDelete = entity.Details;
            if (detailsToDelete != null)
            {
                _manager.Details.DeleteOneDetails(detailsToDelete, trackChanges: true);
            }

            _manager.Driver.DeleteOneDriver(entity);
            _manager.Save();
            return entity;
        }
        public List <Driver> GetAllDriver(bool trackChanges)
        {
            var drivers = _manager.Driver.GetAllDriver(trackChanges).ToList();
            if (!drivers.Any())
            {
                return null;
            }
            foreach (var driver in drivers)
            {
                driver.Details = _manager.Details.GetOneDetailsById(driver.DetailsId, false).FirstOrDefault();
            }
            return drivers;
        }
        public Driver GetOneDriverById(int id, bool trackChanges)
        {
            var driver = _manager.Driver.GetOneDriverById(id, trackChanges).FirstOrDefault();
            if (driver == null)
            {
                return null;
            }
            driver.Details = _manager.Details.GetOneDetailsById(driver.DetailsId, false).FirstOrDefault();
            return driver;
        }
        public void UpdateDriver(bool trackChanges, Driver updatedDriver)
        {
            var drivers = _manager.Driver.GetAllDriver(trackChanges).ToList();
            if (!drivers.Any())
            {
                throw new Exception("There are no drivers to update.");
            }

            foreach (var driver in drivers)
            {
                var details = _manager.Details.GetOneDetailsById(driver.DetailsId, trackChanges).FirstOrDefault();
                if (details == null)
                    continue;

                if (driver.DriverId == updatedDriver.DriverId)
                {
                    driver.DriverId = updatedDriver.DriverId;
                    driver.Plaque = updatedDriver.Plaque;
                    driver.DrivingLicence = updatedDriver.DrivingLicence;
                    driver.Registry = updatedDriver.Registry;
                    driver.ServiceArea = updatedDriver.ServiceArea;
                    details.DetailsId = updatedDriver.Details.DetailsId;
                    details.Name = updatedDriver.Details.Name;
                    details.Email = updatedDriver.Details.Email;
                    details.Bday = updatedDriver.Details.Bday;
                    details.Tc = updatedDriver.Details.Tc;
                    details.PhoneNumber = updatedDriver.Details.PhoneNumber;
                    details.Gender = updatedDriver.Details.Gender;

                    _manager.Driver.UpdateOneDriver(driver);
                    _manager.Details.UpdateOneDetails(details);
                }
            }
        }
    }

}