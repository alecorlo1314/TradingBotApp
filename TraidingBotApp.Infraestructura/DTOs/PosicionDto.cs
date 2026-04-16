using System.Text.Json.Serialization;
using TraidingBotApp.Dominio;

namespace TraidingBotApp.Infraestructura.DTOs;

public record PosicionDto
{
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = "";

    [JsonPropertyName("qty")]
    public decimal Qty { get; set; }

    [JsonPropertyName("avg_entry")]
    public decimal AvgEntry { get; set; }

    [JsonPropertyName("current_price")]
    public decimal CurrentPrice { get; set; }

    [JsonPropertyName("pnl")]
    public decimal Pnl { get; set; }

    [JsonPropertyName("pnl_pct")]
    public decimal PnlPct { get; set; }

    // Propiedades de presentación (opcional)
    public string PnlFormatted => $"{(Pnl >= 0 ? "+" : "")}{Pnl:F2} USD";
    public string PnlPctFormatted => $"{(PnlPct >= 0 ? "+" : "")}{PnlPct:F2}%";
    public Color PnlColor => Pnl >= 0 ? AppColores.Verde : AppColores.Rojo;
}
