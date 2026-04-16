using System.Text.Json.Serialization;

namespace TraidingBotApp.Infraestructura.DTOs;

public class RespuestaOperacionDto
{
    [JsonPropertyName("trades")]
    public List<OperacionDto> Trades { get; set; } = [];
}
