namespace TraidingBotApp.Dominio.Entidades;

public class Balance
{
    public decimal Efectivo { get; set; }
    public decimal ValorPortafolio { get; set; }
    public decimal PoderComprar { get; set; }
    public string? Moneda { get; set; }

    // Constructor
    public Balance(decimal efectivo, decimal valorPortafolio, decimal poderComprar, string? moneda)
    {
        Efectivo = efectivo;
        ValorPortafolio = valorPortafolio;
        PoderComprar = poderComprar;
        Moneda = moneda;
    }
}
