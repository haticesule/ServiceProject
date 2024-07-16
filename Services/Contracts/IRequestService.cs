using Entitiess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IRequestService
    {
        List<Request> GetAllRequest(bool trackChanges);
        List <Request> GetOneRequestById(int id, bool trackChanges);
        Request DeleteOneRequest(int id, bool trackChanges);
        Request CreateOneRequest(Request request);
        void UpdateOneRequest(int id, bool trackChanges, Request updatedRequest);
    }
}
