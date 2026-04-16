using TraidingBotApp.Dominio;

namespace TraidingBotApp.Infraestructura.DTOs;

public class RespuestaSenalDto
{
    public string Symbol { get; set; } = "";
    public string Signal { get; set; } = "HOLD";

    public Color SignalColor => Signal switch
    {
        "BUY" => AppColores.Verde,
        "SELL" => AppColores.Rojo,
        _ => AppColores.Gris,
    };
}
