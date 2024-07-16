using Azure.Core;
using DocumentFormat.OpenXml.Bibliography;
using Entitiess.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using City = Entitiess.Models.City;
using Request = Entitiess.Models.Request;

namespace Repositories.Contracts
{
    public interface IRequestRepository
    {
        IQueryable<Request> GetAllRequest(bool trackChanges);
        IQueryable<Request> GetOneRequestById(int id, bool trackChanges);
        Task<IActionResult> ConfirmRequest(Request request);
        Task SendConfirmationEmail(Request request);
        Request CreateOneRequest(Request request);
        void UpdateOneRequest(Request request);
        void DeleteOneRequest(Request request);


    }
}
