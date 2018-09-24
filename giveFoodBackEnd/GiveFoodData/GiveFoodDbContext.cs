using GiveFoodDataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace GiveFoodData
{
    public class GiveFoodDbContext : IdentityDbContext<User, UserRole, Guid>
    {
        public GiveFoodDbContext(DbContextOptions<GiveFoodDbContext> options) 
            : base(options)
        {

        }
        
        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Document> Documents { get; set; }    
    }
}
