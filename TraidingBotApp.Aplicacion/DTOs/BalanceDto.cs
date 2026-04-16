namespace TraidingBotApp.Aplicacion.DTOs;

public class BalanceDto
{
    public decimal Efectivo { get; set; }
    public decimal ValorPortafolio { get; set; }
    public decimal PoderCompra { get; set; }
    public string Moneda { get; set; } = "USD";
}
