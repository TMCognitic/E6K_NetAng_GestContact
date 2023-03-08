using E6K_NetAng_GestContact.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Runtime.InteropServices.ObjectiveC;

namespace E6K_NetAng_GestContact.Domain
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext()            
        {
            
        }

        public ContactDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Contact> Contacts { get { return Set<Contact>(); } }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
                .EnableSensitiveDataLogging();
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=E6K_NetAng_GestContactEF;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().ToTable("Contact");

            modelBuilder.Entity<Contact>().Property(c => c.Nom)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Contact>().Property(c => c.Prenom)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
