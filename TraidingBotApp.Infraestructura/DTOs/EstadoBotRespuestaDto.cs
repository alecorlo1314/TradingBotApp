namespace TraidingBotApp.Infraestructura.DTOs;

using System.Text.Json.Serialization;

public class EstadoBotRespuestaDto
{
    [JsonPropertyName("running")]
    public bool Running { get; set; }

    [JsonPropertyName("symbol")]
    public string? Symbol { get; set; }

    [JsonPropertyName("capital")]
    public decimal Capital { get; set; }

    [JsonPropertyName("mode")]
    public string? Mode { get; set; }
}

