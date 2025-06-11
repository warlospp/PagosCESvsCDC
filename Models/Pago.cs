namespace PagosCESvsCDC.Models
{
    public class Pago
    {
        public int Id { get; set; }
        public string ClienteId { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public string? MetodoPago { get; set; }
        public DateTime FechaPago { get; set; }
        public string? Estado { get; set; }
    }
}