using Entitiess.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System.Linq;
using System.Linq.Expressions;

namespace Repositories.EFCore
{
    public class CustomerRepository : ICustomerRepository
    {

        private IRepositoryBase<Customer> repositoryBase;

        public CustomerRepository(IRepositoryBase<Customer> repositoryBase)
        {
            this.repositoryBase = repositoryBase;
        }
        public IQueryable<Customer> GetOneCustomerById(int id, bool trackChanges)
        {
            var expression = trackChanges ? (Expression<Func<Customer, bool>>)(c => c.CustomerId == id) : (c => c.CustomerId == id);
            return this.repositoryBase.FindByCondition(expression, trackChanges);
        }
        public IQueryable<Customer> GetAllCustomer(bool trackChanges)
        {
            return this.repositoryBase.FindAll(trackChanges);
        }

        public Customer CreateOneCustomer(Customer customer)
        {
            return this.repositoryBase.Create(customer);
        }

        public void UpdateOneCustomer(Customer customer)
        {
            this.repositoryBase.Update(customer);
        }

        public void DeleteOneCustomer(Customer customer)
        {
            this.repositoryBase.Delete(customer);
        }
    }
}
