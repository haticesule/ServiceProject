using Services.Contracts;
using Entitiess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Contracts;

namespace Services
{
    public class CustomerManager : ICustomerService
    {
        private readonly IRepositoryManager _manager;

        public CustomerManager(IRepositoryManager repositoryManager)
        {
            _manager = repositoryManager;
        }
        public Customer CreateOneCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            Details newDetails = _manager.Details.CreateOneDetails(customer.Details);
            customer.DetailsId = newDetails.DetailsId;

            _manager.Customer.CreateOneCustomer(customer);
            _manager.Save();
            return customer;
        }

        public Customer DeleteOneCustomer(int id, bool trackChanges)
        {
            var entity = GetOneCustomerById(id, trackChanges);

            if (entity == null)
            {
                throw new Exception($"Customer with id:{id} could not be found.");
            }

            var detailsToDelete = entity.Details;
            if (detailsToDelete != null)
            {
                _manager.Details.DeleteOneDetails(detailsToDelete, trackChanges: true);
            }

            _manager.Customer.DeleteOneCustomer(entity);

            _manager.Save();

            return entity;
        }

        public List<Customer> GetAllCustomer(bool trackChanges)
        {
            var customers = _manager.Customer.GetAllCustomer(trackChanges).ToList();
            if (!customers.Any())
            {
                return null;
            }
            foreach (var customer in customers)
            {
                customer.Details = _manager.Details.GetOneDetailsById(customer.DetailsId, false).FirstOrDefault();
            }
            return customers;
        }
        public Customer GetOneCustomerById(int id, bool trackChanges)
        {
            var customer = _manager.Customer.GetOneCustomerById(id, trackChanges).FirstOrDefault();
            if (customer == null)
            {
                return null;
            }
            customer.Details = _manager.Details.GetOneDetailsById(customer.DetailsId, false).FirstOrDefault();
            return customer;
        }

        public void UpdateCustomer(bool trackChanges, Customer updatedCustomer)
        {
            var customers = _manager.Customer.GetAllCustomer(trackChanges).ToList();
            if (!customers.Any())
            {
                throw new Exception("There are no customers to update.");
            }

            foreach (var customer in customers)
            {
                var details = _manager.Details.GetOneDetailsById(customer.DetailsId, trackChanges).FirstOrDefault();
                if (details == null)
                    continue;

                if (customer.CustomerId == updatedCustomer.CustomerId)
                {
                    customer.CustomerId = updatedCustomer.CustomerId;
                    details.DetailsId = updatedCustomer.Details.DetailsId;
                    details.Name = updatedCustomer.Details.Name;
                    details.Email = updatedCustomer.Details.Email;
                    details.Bday = updatedCustomer.Details.Bday;
                    details.Tc = updatedCustomer.Details.Tc;
                    details.PhoneNumber = updatedCustomer.Details.PhoneNumber;
                    details.Gender = updatedCustomer.Details.Gender;

                    _manager.Customer.UpdateOneCustomer(customer);
                    _manager.Details.UpdateOneDetails(details);
                }
            }
        }
    }
}
