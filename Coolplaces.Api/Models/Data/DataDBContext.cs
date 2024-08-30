
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Coolplaces.Api.Models.Data
{
    public class DataDBContext: IdentityDbContext
    {

        public DataDBContext(DbContextOptions<DataDBContext> options)
            : base(options)
        {
        }

        public DbSet<Joint> Joint { get; set; }
    }
}
