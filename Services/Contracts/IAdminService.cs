using Entitiess.Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IAdminService
    {
        List<Admin> GetAllAdmin(bool trackChanges);
        Admin GetOneAdminById(int id, bool trackChanges);
        Admin DeleteOneAdmin(int id, bool trackChanges);
        void UpdateAdmin(bool trackChanges, Admin updatedAdmin);
        bool CreateOneAdmin(Admin admin);
        (bool, string) AuthenticateAdmin(LoginRequest loginRequest);
    }
}
