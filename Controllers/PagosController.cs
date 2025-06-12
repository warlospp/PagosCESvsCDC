using Microsoft.AspNetCore.Mvc;
using PagosCESvsCDC.Models;
using PagosCESvsCDC.Data;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class PagosController : ControllerBase
{
    private readonly ApplicationDbContextCes _cesContext;
    private readonly ApplicationDbContextCdc _cdcContext;

    public PagosController(ApplicationDbContextCes cesContext, ApplicationDbContextCdc cdcContext)
    {
        _cesContext = cesContext;
        _cdcContext = cdcContext;
    }

    [HttpPost("insertar-masivo")]
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

        var pagosParaCdc = pagos.Select(p => new Pago
        {
            ClienteId = p.ClienteId,
            Monto = p.Monto,
            MetodoPago = p.MetodoPago,
            FechaPago = p.FechaPago,
            Estado = p.Estado
        }).ToList();

        await _cesContext.Pagos.AddRangeAsync(pagos);
        await _cesContext.SaveChangesAsync();

        await _cdcContext.Pagos.AddRangeAsync(pagosParaCdc);
        await _cdcContext.SaveChangesAsync();

        return Ok("Datos Gurdados...");
    }
}
