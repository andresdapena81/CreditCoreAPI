using Microsoft.EntityFrameworkCore;
using CreditCore.Domain.Entities;

namespace CreditCore.Infrastructure.Persistence
{
    public class CreditDbContext : DbContext
    {
        public CreditDbContext(DbContextOptions<CreditDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Credito> Creditos { get; set; }
        public DbSet<PlanPago> PlanPagos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Credito>()
                .HasIndex(c => c.NumeroCredito)
                .IsUnique();

            modelBuilder.Entity<Credito>()
                .HasOne(c => c.Cliente)
                .WithMany(c => c.Creditos)
                .HasForeignKey(c => c.ClienteId);

            modelBuilder.Entity<PlanPago>()
                .HasOne(p => p.Credito)
                .WithMany(c => c.PlanPagos)
                .HasForeignKey(p => p.CreditoId);
        }
    }
}
