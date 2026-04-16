using TraidingBotApp.Dominio;

namespace TraidingBotApp.Dominio.Entidades;

public class Posicion
{
    public string? Simbolo { get; set; } // AAPL, MSFT, SP500
    public decimal Cantidad { get; set; }
    public decimal PrecioEntradaPromedio { get; set; }
    public decimal PrecioActual { get; set; }
    public decimal GananciaPerdida { get; set; }
    public decimal GananciaPerdidaPct { get; set; }

    public Posicion(string? simbolo, decimal cantidad, decimal precioEntradaPromedio, decimal precioActual, decimal gananciaPerdida, decimal gananciaPerdidaPct)
    {
        Simbolo = simbolo;
        Cantidad = cantidad;
        PrecioEntradaPromedio = precioEntradaPromedio;
        PrecioActual = precioActual;
        GananciaPerdida = gananciaPerdida;
        GananciaPerdidaPct = gananciaPerdidaPct;
    }
}
