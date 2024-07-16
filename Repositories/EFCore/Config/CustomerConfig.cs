using Entitiess.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore.Config
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasData(
                new Customer { DetailsId = 1, CustomerId = 1 },
                new Customer { DetailsId = 2, CustomerId = 2 },
                new Customer { DetailsId = 3, CustomerId = 3 }

                );
        }
    }
}
