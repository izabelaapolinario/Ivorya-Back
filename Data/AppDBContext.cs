using ivorya_back.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ivorya_back.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _schema;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _schema = configuration.GetSection("DatabaseSettings:Schema").Value ?? "public";
        }

        public DbSet<Contato> Contatos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(_schema);

            modelBuilder.Entity<Contato>()
        .HasKey(c => c.IdContato);

            modelBuilder.Entity<Contato>()
       .Property(c => c.IdContato)
       .ValueGeneratedOnAdd();

            modelBuilder.Entity<Contato>()
       .Property(c => c.IdContato)
       .UseIdentityColumn();

            base.OnModelCreating(modelBuilder);
        }
    }
}
