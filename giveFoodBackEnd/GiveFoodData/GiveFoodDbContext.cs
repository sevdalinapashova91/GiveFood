using GiveFoodDataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace GiveFoodData
{
    public class GiveFoodDbContext : IdentityDbContext<User, ApplicationRole, string>
    {
        public GiveFoodDbContext(DbContextOptions<GiveFoodDbContext> options) 
            : base(options)
        {

        }
        public DbSet<User> Users { get; set; }

        public DbSet<IdentityRole> Roles { get; set; }

        public DbSet<Document> Documents { get; set; }

    }
}
