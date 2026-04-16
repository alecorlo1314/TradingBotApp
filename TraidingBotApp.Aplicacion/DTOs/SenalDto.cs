using TraidingBotApp.Dominio;

namespace TraidingBotApp.Aplicacion.DTOs;

public record SenalDto
{
    public string Simbolo { get; set; } = "";
    public string Senal { get; set; } = "HOLD";

    public Color ColorSenal => Senal switch
    {
        "COMPRAR" => AppColores.Verde,
        "VENDER" => AppColores.Rojo,
        _ => AppColores.Gris,
    };

    public Color ColorSenalBackGroundColor => Senal switch
    {
        "COMPRAR" => AppColores.VerdeBackGroundColor,
        "VENDER" => AppColores.RojoBackGroundColor,
        _ => AppColores.GrisBackGroundColor,
    };
}
