namespace TraidingBotApp.Dominio.Entidades;

public class PuntoPrecio
{
    public string Fecha { get; set; } = "";
    public decimal Cierre { get; set; }

    // Constructor
    public PuntoPrecio(string fecha, decimal cierre)
    {
        Fecha = fecha;
        Cierre = cierre;
    }
}
