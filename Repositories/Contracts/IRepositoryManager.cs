using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        ICustomerRepository Customer { get; }
        IDriverRepository Driver { get; }
        IDetailsRepository Details { get; }
        IRequestRepository Request { get; }
        ICityRepository City { get; }
        ITownRepository Town { get; }
        ILocalityRepository Locality { get; }
        INeighborhoodRepository Neighborhood { get; }
        IAdminRepository Admin { get; }
        

        void Save();
    }
}
