using TraidingBotApp.Dominio;

namespace TraidingBotApp.Aplicacion.DTOs;

public record OperacionDto
{
    public string Id { get; set; } = "";
    public string Simbolo { get; set; } = "";
    public string Lado { get; set; } = "";
    public decimal CantidadEjecutada { get; set; }
    public decimal PrecioPromedioEjecutado { get; set; }
    public string Estado { get; set; } = "";
    public string CreadoEn { get; set; } = "";
    public string LadoFormateado => Lado.ToUpper();
    public string FechaFormateada =>

    DateTimeOffset.Parse(CreadoEn)
        .ToLocalTime()
        .ToString("MMM dd, yyyy");

    public string HoraFormateada =>
        DateTimeOffset.Parse(CreadoEn)
        .ToLocalTime()
        .ToString("HH:mm");

    public Color ColorLado => Lado.ToLower() == "comprar"
        ? AppColores.Verde
        : AppColores.Rojo;
}
