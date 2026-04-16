using System.Text.Json.Serialization;

namespace TraidingBotApp.Infraestructura.DTOs;

public class RespuestaIndicadorDto
{
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = "";
    [JsonPropertyName("rsi")]
    public decimal Rsi { get; set; }
    [JsonPropertyName("macd")]
    public decimal Macd { get; set; }
    [JsonPropertyName("macd_signal")]
    public decimal MacdSignal { get; set; }
    [JsonPropertyName("macd_hist")]
    public decimal MacdHist { get; set; }
    [JsonPropertyName("close")]
    public decimal Close { get; set; }
}
