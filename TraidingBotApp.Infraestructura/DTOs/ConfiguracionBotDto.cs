using System.Text.Json.Serialization;

namespace TraidingBotApp.Infraestructura.DTOs;

public class ConfiguracionBotDto
{
    [JsonPropertyName("symbol")]
    public string Simbolo { get; set; } = "AAPL";
    [JsonPropertyName("capital")]
    public decimal Capital { get; set; } = 5;
    [JsonPropertyName("stop_loss")]
    public decimal StopLoss { get; set; } = 2;
    [JsonPropertyName("mode")]
    public string Modo { get; set; } = "moderado";
    [JsonPropertyName("interval_minutes")]
    public int IntervaloMinutos { get; set; } = 5;
}
