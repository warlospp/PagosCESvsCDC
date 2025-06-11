using Microsoft.AspNetCore.Mvc;
using PagosCESvsCDC.Models;
using PagosCESvsCDC.Data;
using Microsoft.EntityFrameworkCore;

namespace PagosCESvsCDC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagosController : ControllerBase
    {
        private readonly ApplicationDbContext _cesContext;
        private readonly ApplicationDbContext _cdcContext;

        public PagosController(ApplicationDbContext cesContext, ApplicationDbContext cdcContext)
        {
            _cesContext = cesContext;
            _cdcContext = cdcContext;
        }

        [HttpPost("insertar-masivo")] // Inserta en ambas bases
        public async Task<IActionResult> InsertarMasivo()
        {
            var pagos = new List<Pago>();
            var rnd = new Random();
            for (int i = 0; i < 1000; i++)
            {
                pagos.Add(new Pago
                {
                    ClienteId = $"Cliente{i}",
                    Monto = rnd.Next(100, 10000),
                    MetodoPago = "Tarjeta",
                    FechaPago = DateTime.UtcNow,
                    Estado = "Procesado"
                });
            }

            await _cesContext.Pagos.AddRangeAsync(pagos);
            await _cdcContext.Pagos.AddRangeAsync(pagos);
            await _cesContext.SaveChangesAsync();            
            await _cdcContext.SaveChangesAsync();
            return Ok(new { mensaje = "Inserción masiva realizada en ambas bases de datos." });
        }
    }
}