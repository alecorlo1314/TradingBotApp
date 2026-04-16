namespace TraidingBotApp.Dominio.Entidades;

public class Indicadores
{
    public string Symbol { get; set; } = "";
    public decimal Rsi { get; set; }
    public decimal Macd { get; set; }
    public decimal MacdSignal { get; set; }
    public decimal MacdHist { get; set; }
    public decimal Close { get; set; }

    //Constructor
    public Indicadores(string symbol, decimal rsi, decimal macd, decimal macdSignal, decimal macdHist, decimal close)
    {
        Symbol = symbol;
        Rsi = rsi;
        Macd = macd;
        MacdSignal = macdSignal;
        MacdHist = macdHist;
        Close = close;
    }
}
