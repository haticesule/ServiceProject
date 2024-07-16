using Azure.Core;
using Entitiess.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.EFCore.Config;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;


namespace Repositories.EFCore
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext()
        {
        }
        public RepositoryContext(DbContextOptions options) :
            base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Driver> Driver { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Town> Town { get; set; }
        public DbSet<Locality> Locality { get; set; }
        public DbSet<Neighborhood> Neighborhood { get; set; }
        public DbSet<Entitiess.Models.Request> Request { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<AdminType> AdminType { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Details> Details { get; set; }
        public DbSet<LoginRequest> LoginRequest { get; set; }
        public object Results { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
