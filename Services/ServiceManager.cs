using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICustomerService> _customerservice;
        private readonly Lazy<IDriverService> _driverService;
        private readonly Lazy<IDetailsService> _detailsService;
        private readonly Lazy<IRequestService> _requestService;
        private readonly Lazy<ICityService> _cityService;
        private readonly Lazy<ITownService> _townService;
        private readonly Lazy<ILocalityService> _localityService;
        private readonly Lazy<INeighborhoodService> _nborhoodService;
        private readonly Lazy<IAdminService> _adminService;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _customerservice = new Lazy<ICustomerService>(()=> new CustomerManager(repositoryManager));
            
            _detailsService = new Lazy<IDetailsService>(()=> new DetailManager(repositoryManager));
            _requestService = new Lazy<IRequestService>(()=> new RequestManager(repositoryManager, this));
            _cityService = new Lazy<ICityService>(() => new CityManager(repositoryManager));
            _townService = new Lazy<ITownService>(()=> new TownManager(repositoryManager));
            _localityService = new Lazy<ILocalityService>(() => new LocalityManager(repositoryManager));
            _nborhoodService = new Lazy<INeighborhoodService>(()=>new NeighborhoodManager(repositoryManager));
            var issuer = "JokenTokenAuthorize";
            var audience = "JokenTokenAuthorize";
            var secretKey = "A247DB24-C8AE-4B8A-8CB2-59637754BF2F";
            JwtService jwtService = new JwtService(issuer, audience, secretKey);
            _adminService = new Lazy<IAdminService>(()=>new AdminManager(repositoryManager, jwtService));
            _driverService = new Lazy<IDriverService>(() => new DriverManager(repositoryManager, jwtService));
        }
        public ICustomerService CustomerService => _customerservice.Value;
        public IDriverService DriverService => _driverService.Value;
        public IDetailsService DetailsService => _detailsService.Value;
        public IRequestService RequestService => _requestService.Value;
        public ICityService CityService => _cityService.Value;
        public ITownService TownService => _townService.Value;
        public ILocalityService LocalityService => _localityService.Value;
        public INeighborhoodService NeighborhoodService => _nborhoodService.Value;
        public IAdminService AdminService => _adminService.Value;
    }
}
