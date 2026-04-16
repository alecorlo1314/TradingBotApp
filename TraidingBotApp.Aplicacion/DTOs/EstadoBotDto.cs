namespace TraidingBotApp.Aplicacion.DTOs;

public record EstadoBotDto
{
    public bool Ejecutando { get; set; }
    public string? Simbolo { get; set; }
    public decimal Capital { get; set; }
    public string? Modo { get; set; }
}
