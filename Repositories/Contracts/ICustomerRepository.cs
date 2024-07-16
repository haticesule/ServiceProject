using Entitiess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface ICustomerRepository
    {
        IQueryable<Customer> GetAllCustomer(bool trackChanges);
        IQueryable<Customer> GetOneCustomerById(int id, bool trackChanges);
        Customer CreateOneCustomer(Customer customer);
        void UpdateOneCustomer(Customer customer);
        void DeleteOneCustomer(Customer customer);
    
    }
}
