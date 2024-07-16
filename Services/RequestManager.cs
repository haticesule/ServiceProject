using Entitiess.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RequestManager : IRequestService
    {
        private readonly IRepositoryManager _manager;
        private readonly IServiceManager serviceManager;
        public RequestManager(IRepositoryManager repositoryManager, IServiceManager serviceManager)
        {
            _manager = repositoryManager;
            this.serviceManager = serviceManager;
        }

        public Request CreateOneRequest(Request request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Customer == null)
                throw new ArgumentNullException(nameof(request.Customer));

            if (request.Neighborhood == null)
                throw new ArgumentNullException(nameof(request.Neighborhood));

            
            Customer newCustomer = _manager.Customer.CreateOneCustomer(request.Customer);
            if (newCustomer == null)
                throw new Exception("Failed to create new customer.");

            
            Neighborhood newNeighborhood = _manager.Neighborhood.CreateOneNeighborhood(request.Neighborhood);
            if (newNeighborhood == null)
                throw new Exception("Failed to create new neighborhood.");

            request.CustomerId = newCustomer.CustomerId;
            request.NeighborhoodId = newNeighborhood.NeighborhoodId;
            request.DestinationTown = newNeighborhood.Locality?.Town?.TownName;
            request.DestinationLocality = newNeighborhood.Locality?.LocalityName;
            request.DestinationNeighborhood = newNeighborhood.NeighborhoodName;

            _manager.Request.CreateOneRequest(request);
            _manager.Save();

            return request;
        }

        public Request DeleteOneRequest(int id, bool trackChanges)
        {
            var entity = _manager.Request.GetOneRequestById(id, trackChanges).SingleOrDefault();
            if (entity == null)
                throw new Exception($"Request with id:{id} could not found.");

            var customerDelete = entity.Customer;
            if (customerDelete != null)
            {
                _manager.Customer.DeleteOneCustomer(customerDelete);
            }

       
            _manager.Request.DeleteOneRequest(entity);
            _manager.Save();
            return entity;
        }



        public List <Request> GetAllRequest(bool trackChanges)
        {
            var request = _manager.Request.GetAllRequest(trackChanges).ToList();
            if(!request.Any())
            {
                return null;
            }
            foreach(var _request in request)
            {
                _request.Customer = serviceManager.CustomerService.GetOneCustomerById(_request.CustomerId, false);

                _request.Neighborhood = serviceManager.NeighborhoodService.GetOneNeighborhoodById(_request.NeighborhoodId, false);


                Customer myCustomer = serviceManager.CustomerService.GetOneCustomerById(_request.CustomerId, false);
                _request.Customer.Details.Name = myCustomer.Details.Name;


                int townId = Int32.Parse(_request.DestinationTown);
                Town myTown = serviceManager.TownService.GetOneTownById(townId, false);
                _request.DestinationTown = myTown.TownName;

                int localityId = Int32.Parse(_request.DestinationLocality);
                Locality myLocality = serviceManager.LocalityService.GetOneLocalityById(localityId, false);
                _request.DestinationLocality = myLocality.LocalityName;

                int neighborhoodId = Int32.Parse(_request.DestinationNeighborhood);
                Neighborhood myNeighborhood = serviceManager.NeighborhoodService.GetOneNeighborhoodById(neighborhoodId, false);
                _request.DestinationNeighborhood = myNeighborhood.NeighborhoodName;
            }
            return (List<Request>)request;
        }

        public List<Request> GetOneRequestById(int id, bool trackChanges)
        {
            var requestById = _manager.Request.GetOneRequestById(id, false).FirstOrDefault();
            if (requestById == null)
            {
                return null;
            }
            requestById.Customer = serviceManager.CustomerService.GetOneCustomerById(requestById.CustomerId, false);
            requestById.Neighborhood = serviceManager.NeighborhoodService.GetOneNeighborhoodById(requestById.NeighborhoodId, false);
            return new List<Request> { requestById };
        }


        public void UpdateOneRequest(int id, bool trackChanges, Request updatedRequest)
        {
            var requestUpdate = _manager.Request.GetAllRequest(trackChanges).ToList();
            if(!requestUpdate.Any())
            {
                throw new Exception("There are no Request to update.");
            }
            foreach(var _request in requestUpdate)
            {
                var customer = _manager.Customer.GetOneCustomerById(_request.CustomerId, trackChanges).FirstOrDefault();
                if (customer == null)
                    continue;

                if(_request.CustomerId == updatedRequest.CustomerId)
                {
                    _request.RequestId = updatedRequest.RequestId;
                    customer.CustomerId = updatedRequest.CustomerId;
                    _request.NeighborhoodId = updatedRequest.NeighborhoodId;
                    _request.DestinationTown = updatedRequest.DestinationTown;
                    _request.BookingTime = updatedRequest.BookingTime;
                    _request.BookingDate = updatedRequest.BookingDate;

                    _manager.Request.UpdateOneRequest(_request);
                    _manager.Customer.UpdateOneCustomer(customer);
                }

                var neighborhood = _manager.Neighborhood.GetOneNeighborhoodById(_request.NeighborhoodId, trackChanges).FirstOrDefault();
                if(neighborhood == null)
                    continue ;
                
                if(_request.NeighborhoodId== updatedRequest.NeighborhoodId)
                {

                }
            }
        }
    }
}
