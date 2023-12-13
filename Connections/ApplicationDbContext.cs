using MyContacts.Entities;
using Microsoft.EntityFrameworkCore;

namespace MyContacts.Connections
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Contatto> Contatti { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}


