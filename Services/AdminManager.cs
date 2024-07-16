using Entitiess.Models;
using Microsoft.IdentityModel.Tokens;
using Repositories.EFCore;
using Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using Repositories.Contracts;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Diagnostics.CodeAnalysis;

namespace Services
{
    public class AdminManager : IAdminService
    {
        private readonly JwtService _jwtService;
        private readonly ILogger _logger;
        private IRepositoryManager _manager;
        public AdminManager(IRepositoryManager repositoryManager, JwtService jwtService)
        {
            _manager = repositoryManager;
            _jwtService = jwtService;
        }

        public bool CreateOneAdmin(Admin admin)
        {
            if(admin == null)
                throw new ArgumentNullException(nameof(admin));

            Details newDetails = _manager.Details.CreateOneDetails(admin.Details);

            _manager.Admin.CreateOneAdmin(admin);
            _manager.Save();
            return true;
        }

        public (bool, string) AuthenticateAdmin(LoginRequest loginRequest)
        {
            var adminList = GetAllAdmin(false).ToList();
            var admin = adminList.FirstOrDefault(admin => admin.Details.Email == loginRequest.Email);

            if (admin == null)
            {
                return (false, "Admin not found for email");
            }
            else
            {
                if (admin.Password.Equals(loginRequest.Password))
                {
                    var details = admin.Details.Email;
                    return (true, _jwtService.GenerateJwt(admin.AdminId, details));
                }
                else
                {
                    return (false, "Wrong password");
                }
            }
        }

        public List<Admin> GetAllAdmin(bool trackChanges)
        {
            var admins = _manager.Admin.GetAllAdmin(trackChanges).ToList();
            if (!admins.Any())
            {
                return null;
            }
            foreach(var admin in admins)
            {
                admin.Details = _manager.Details.GetOneDetailsById(admin.DetailsId, false).FirstOrDefault();
            }
            return admins;

        }

        public Admin GetOneAdminById(int id, bool trackChanges)
        {
            var admin = _manager.Admin.GetOneAdminById(id, trackChanges).FirstOrDefault();
            if (admin == null)
            {
                return null;
            }
            admin.Details = _manager.Details.GetOneDetailsById(admin.DetailsId, false).FirstOrDefault();
            return admin;
        }

        public Admin DeleteOneAdmin(int id, bool trackChanges)
        {
            var entity = GetOneAdminById(id, trackChanges);
            if(entity == null)
            {
                throw new Exception($"Admin with id:{id} could not be found.");
            }
            var detailToDelete = entity.Details;
            if(detailToDelete != null)
            {
                _manager.Details.DeleteOneDetails(detailToDelete, trackChanges: true);
            }
            _manager.Admin.DeleteOneAdmin(entity, false);
            _manager.Save();
            return entity;
        }

        public void UpdateAdmin(bool trackChanges, Admin updatedAdmin)
        {
            var admin = _manager.Admin.GetAllAdmin(trackChanges).ToList();
            if (!admin.Any())
            {
                throw new Exception("There are no admin to update.");
            }

            foreach (var admins in admin)
            {
                var details = _manager.Details.GetOneDetailsById(admins.DetailsId, trackChanges).FirstOrDefault();
                if (details == null)
                    continue;

                if (admins.AdminId == updatedAdmin.AdminId)
                {
                    admins.AdminId = updatedAdmin.AdminId;
                    admins.TypeId = updatedAdmin.TypeId;
                    admins.AdminType = updatedAdmin.AdminType;
                    admins.Password = updatedAdmin.Password;
                    details.DetailsId = updatedAdmin.DetailsId;
                    details.Name = updatedAdmin.Details.Name;
                    details.Email = updatedAdmin.Details.Email;
                    details.Bday = updatedAdmin.Details.Bday;
                    details.Tc = updatedAdmin.Details.Tc;
                    details.PhoneNumber = updatedAdmin.Details.PhoneNumber;
                    details.Gender = updatedAdmin.Details.Gender;

                    _manager.Admin.UpdateOneAdmin(admins);
                    _manager.Details.UpdateOneDetails(details);
                }
            }
        }
    }

    public class JwtService
    {
        private readonly string _issuer;
        private readonly string _audience;
        private readonly string _secretKey;

        public JwtService()
        {

        }

        public JwtService(string issuer, string audience, string secretKey)
        {
            _issuer = issuer;
            _audience = audience;
            _secretKey = secretKey;

        }

        public string GenerateJwt(long userId, string userEmail)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, userEmail),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var encodedToken = tokenHandler.WriteToken(token);

            return encodedToken;
        }
    }
}
