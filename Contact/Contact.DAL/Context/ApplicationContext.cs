using Microsoft.EntityFrameworkCore;
using Contact.DAL.Modell;

namespace Contact.DAL.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<ContactInfo> Contacts { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
    }
}
