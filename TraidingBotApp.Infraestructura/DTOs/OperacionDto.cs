using System.Text.Json.Serialization;
using TraidingBotApp.Dominio;

namespace TraidingBotApp.Infraestructura.DTOs;

public class OperacionDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = "";

    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = "";

    [JsonPropertyName("side")]
    public string Side { get; set; } = "";

    [JsonPropertyName("filled_qty")]
    public decimal FilledQty { get; set; }

    [JsonPropertyName("filled_avg")]
    public decimal FilledAvg { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; } = "";

    [JsonPropertyName("created_at")]
    public string CreatedAt { get; set; } = "";

    public string SideFormatted => Side.ToUpper();
    public Color SideColor => Side.ToLower() == "buy"
        ? AppColores.Verde
        : AppColores.Rojo;
}
