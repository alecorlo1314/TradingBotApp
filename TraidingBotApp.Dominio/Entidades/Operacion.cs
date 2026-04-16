using TraidingBotApp.Dominio;

namespace TraidingBotApp.Dominio.Entidades;

public class Operacion
{
    public string Id { get; set; } = "";
    public string Simbolo { get; set; } = "";
    public string Lado { get; set; } = "";
    public decimal CantidadEjecutada { get; set; }
    public decimal PrecioPromedioEjecutado { get; set; }
    public string Estado { get; set; } = "";
    public string CreadoEn { get; set; } = "";
}
