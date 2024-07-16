using Entitiess.Models;
using Microsoft.Extensions.Logging;
using Repositories.Contracts;
using Repositories.EFCore;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _context;
    private readonly Lazy<ICustomerRepository> _customerRepository;
    private readonly Lazy<IDriverRepository> _driverRepository;
    private readonly Lazy<IDetailsRepository> _detailsRepository;
    private readonly Lazy<IRequestRepository> _requestRepository;
    private readonly Lazy<ICityRepository> _cityRepository;
    private readonly Lazy<ITownRepository> _townRepository;
    private readonly Lazy<ILocalityRepository> _localityRepository;
    private readonly Lazy<INeighborhoodRepository> _nborhoodRepository;
    private readonly Lazy<IAdminRepository> _adminRepository;
    private readonly ILogger<Admin> _logger;

    public RepositoryManager(RepositoryContext context, IRepositoryBase<Customer> customerRepositoryBase, IRepositoryBase<Driver> driverRepository, IRepositoryBase<Details> detailsRepository, IRepositoryBase<Request> requestRepository, IRepositoryBase<City> cityRepository, IRepositoryBase<Town> townRepository, IRepositoryBase<Locality> localityRepository, IRepositoryBase<Neighborhood> neighborhoodRepsitory, IRepositoryBase<Admin> adminRepository)
    {
        _context = context;

        _customerRepository = new Lazy<ICustomerRepository>(() => new CustomerRepository(customerRepositoryBase));
        _driverRepository = new Lazy<IDriverRepository>(() => new DriverRepository(_context, driverRepository));
        _detailsRepository = new Lazy<IDetailsRepository>(() => new DetailsRepository(_context, detailsRepository, adminRepository));
        _requestRepository = new Lazy<IRequestRepository>(() => new RequestRepository(_context, requestRepository));
        _cityRepository = new Lazy<ICityRepository>(() => new CityRepository(_context, cityRepository));
        _townRepository = new Lazy<ITownRepository>(() => new TownRepository(_context, townRepository));
        _localityRepository = new Lazy<ILocalityRepository>(() => new LocalityRepository(_context, localityRepository));
        _nborhoodRepository = new Lazy<INeighborhoodRepository>(() => new NeighborhoodRepository(_context, neighborhoodRepsitory));
        _adminRepository = new Lazy<IAdminRepository>(() => new AdminRepository(_context ,adminRepository));
    }
    public ICustomerRepository Customer => _customerRepository.Value;
    public IDriverRepository Driver => _driverRepository.Value;
    public IDetailsRepository Details => _detailsRepository.Value;
    public IRequestRepository Request => _requestRepository.Value;
    public ICityRepository City => _cityRepository.Value;
    public ITownRepository Town => _townRepository.Value;
    public ILocalityRepository Locality => _localityRepository.Value;
    public INeighborhoodRepository Neighborhood => _nborhoodRepository.Value;
    public IAdminRepository Admin => _adminRepository.Value;

    public void Save()
    {
        _context.SaveChanges();
    }
}