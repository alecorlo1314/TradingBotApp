using System.Text.Json.Serialization;
using TraidingBotApp.Dominio.Entidades;

namespace TraidingBotApp.Infraestructura.DTOs;

public class RespuestaHitorialDto
{
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = "";

    [JsonPropertyName("period")]
    public string Periodo { get; set; } = "";
    [JsonPropertyName("data")]
    public List<PuntoPrecioDto> Data { get; set; } = [];
}
