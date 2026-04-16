using System.Text.Json.Serialization;

namespace TraidingBotApp.Infraestructura.DTOs;

public record RespuestaBalanceDto
{
    [JsonPropertyName("cash")]
    public decimal Cash { get; set; }

    [JsonPropertyName("portfolio_value")]
    public decimal PortfolioValue { get; set; }

    [JsonPropertyName("buying_power")]
    public decimal BuyingPower { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; } = "USD";
}
