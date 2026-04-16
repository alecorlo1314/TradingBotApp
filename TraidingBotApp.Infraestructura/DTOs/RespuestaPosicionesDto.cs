using System.Text.Json.Serialization;

namespace TraidingBotApp.Infraestructura.DTOs;

public class RespuestaPosicionesDto
{
    [JsonPropertyName("positions")]
    public List<PosicionDto>? Posiciones { get; set; } = [];
}