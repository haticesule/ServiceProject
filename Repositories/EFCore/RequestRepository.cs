using Entitiess.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RequestRepository : IRequestRepository
    {
        private readonly IRepositoryBase<Request> requestRepositoryBase;
        private readonly RepositoryContext _context;

        public RequestRepository(RepositoryContext context, IRepositoryBase<Request> requestRepository)
        {
            _context = context;
            this.requestRepositoryBase = requestRepository;
        }

        public Task<IActionResult> ConfirmRequest(Request request)
        {
            throw new NotImplementedException();
        }

        public Request CreateOneRequest(Request request)
        {
            return requestRepositoryBase.Create(request);
        }

        public void DeleteOneRequest(Request request)
        {
            requestRepositoryBase.Delete(request);
        }

        public IQueryable<Request> GetAllRequest(bool trackChanges)
        {
            return requestRepositoryBase.FindAll(trackChanges);
        }

        public IQueryable<Request> GetOneRequestById(int id, bool trackChanges)
        {
            var expression = trackChanges ? (Expression<Func<Request, bool>>)(c => c.RequestId == id) : (c => c.RequestId == id);
            return requestRepositoryBase.FindByCondition(expression, trackChanges);
        }

        public Task SendConfirmationEmail(Request request)
        {
            throw new NotImplementedException();
        }

        public void UpdateOneRequest(Request request)
        {
            requestRepositoryBase.Update(request);
        }


    }
}
