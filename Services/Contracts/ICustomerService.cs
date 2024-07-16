using Entitiess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ICustomerService
    {
        List<Customer> GetAllCustomer(bool trackChanges);
        Customer GetOneCustomerById(int id, bool trackChanges);
        Customer DeleteOneCustomer(int id, bool trackChanges);
        Customer CreateOneCustomer(Customer customer);
        void UpdateCustomer(bool trackChanges, Customer updatedCustomer);
    }
}
