namespace TraidingBotApp.Aplicacion.DTOs;

public record PrecioGraficaDto
{
    public string Fecha { get; set; } = "";
    public decimal Cierre { get; set; }
}
