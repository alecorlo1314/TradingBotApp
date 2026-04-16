using TraidingBotApp.Dominio;

namespace TraidingBotApp.Aplicacion.DTOs;

public class PosicionDto
{
    public string? Simbolo { get; set; } // AAPL, MSFT, SP500
    //La cantidad de acciones que posees
    //Es una fracción de acción
    public decimal Cantidad { get; set; }
    //El precio promedio al entrar en una posicion
    public decimal PrecioEntradaPromedio { get; set; }
    // El precio actual de mercado de la acción
    public decimal PrecioActual { get; set; }
    // “Profit and Loss” en valor absoluto.
    // Es la ganancia o pérdida actual en dólares (o la moneda de la cuenta).
    public decimal GananciaPerdida { get; set; }
    // La ganancia o pérdida expresada en porcentaje respecto al precio de entrada.
    public decimal GananciaPerdidaPct { get; set; }

    public string GananciaPerdidaFormateada =>
$"{(GananciaPerdida >= 0 ? "+" : "")}{GananciaPerdida:F2} USD";
    public string GananciaPerdidaPctFormateada =>
        $"{(GananciaPerdidaPct >= 0 ? "+" : "")}{GananciaPerdidaPct:F2}%";
    public Color ColorGananciaPerdida =>
        GananciaPerdida >= 0 ? AppColores.Verde : AppColores.Rojo;
}