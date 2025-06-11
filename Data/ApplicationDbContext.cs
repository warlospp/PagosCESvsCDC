using Microsoft.EntityFrameworkCore;
using PagosCESvsCDC.Models;

namespace PagosCESvsCDC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Pago> Pagos { get; set; }
    }
}