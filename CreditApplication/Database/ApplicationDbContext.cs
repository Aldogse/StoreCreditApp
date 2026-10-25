using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Users;


namespace CreditApplication.Database
{
    public class ApplicationDbContext: IdentityDbContext<LoginCredentials>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)
        {
            
        }

       public DbSet<Credit> Credits { get; set; }
       public DbSet <Customers> Customers { get; set; }
       public DbSet<Products> Products { get; set; }
    }
}
