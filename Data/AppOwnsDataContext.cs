using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AppOwnsData.Models
{
    public class AppOwnsDataContext : DbContext
    {
        public AppOwnsDataContext (DbContextOptions<AppOwnsDataContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Department { get; set; } = default!;
        public DbSet<Seller> Seller { get; set; }
        public DbSet<SalesRecord> SalesRecord{ get; set; }
    }
}
