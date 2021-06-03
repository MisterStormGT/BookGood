using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext()
        {
        }
        public SchoolContext(DbContextOptions<SchoolContext> options):base(options)
        {
        }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Section>().ToTable("Sec");
            modelBuilder.Entity<Publisher>().ToTable("Pub");
            modelBuilder.Entity<Author>().ToTable("Authore");
            modelBuilder.Entity<Book>().ToTable("Booked");

        }

    }
}
