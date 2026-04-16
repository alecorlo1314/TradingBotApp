namespace TraidingBotApp.Dominio.Entidades;

public class PrecioGrafica
{
    public string Date { get; set; } = "";
    public decimal Close { get; set; }

    // Constructor
    public PrecioGrafica(string date, decimal close)
    {
        Date = date;
        Close = close;
    }
}
