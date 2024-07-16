using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IServiceManager
    {
        ICustomerService CustomerService { get; }
        IDriverService DriverService { get; }
        IDetailsService DetailsService { get; }
        IRequestService RequestService { get; }
        ICityService CityService { get; }
        ITownService TownService { get; }
        ILocalityService LocalityService { get; }
        INeighborhoodService NeighborhoodService { get; }
        IAdminService AdminService { get; }
    }
}
