using Microsoft.EntityFrameworkCore;
using PagosCESvsCDC.Models;

namespace PagosCESvsCDC.Data
{
    public class ApplicationDbContextCdc : DbContext
    {
        public ApplicationDbContextCdc(DbContextOptions<ApplicationDbContextCdc> options) : base(options) { }

        public DbSet<Pago> Pagos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pago>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }
    }
}