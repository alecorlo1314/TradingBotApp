namespace TraidingBotApp.Aplicacion.DTOs;

public class IndicadoresDto
{
    public string Simbolo { get; set; } = "";
    public decimal Rsi { get; set; }
    public decimal Macd { get; set; }
    public decimal MacdSenal { get; set; }
    public decimal MacdHist { get; set; }
    public decimal Cierre { get; set; }

    // RSI color según nivel
    public Color RsiColor => Rsi switch
    {
        < 35 => Color.FromArgb("#00e5a0"),  // sobrevendido → verde (oportunidad compra)
        > 65 => Color.FromArgb("#ff6b35"),  // sobrecomprado → naranja (posible venta)
        _ => Color.FromArgb("#0090ff"),  // neutral → azul
    };

    public string RsiDescripcion => Rsi switch
    {
        < 35 => "Sobrevendido",
        > 65 => "Sobrecomprado",
        _ => "Neutral",
    };

    // MACD color según histograma
    public Color MacdHistColor => MacdHist >= 0
        ? Color.FromArgb("#00e5a0")
        : Color.FromArgb("#ff6b35");

}
