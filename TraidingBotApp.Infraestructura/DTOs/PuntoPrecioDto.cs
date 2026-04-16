using System.Text.Json.Serialization;

namespace TraidingBotApp.Infraestructura.DTOs;

public class PuntoPrecioDto
{
    [JsonPropertyName("date")]
    public string Date { get; set; } = "";

    [JsonPropertyName("close")]
    public decimal Close { get; set; }
}
