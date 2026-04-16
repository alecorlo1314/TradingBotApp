namespace TraidingBotApp.Aplicacion.DTOs;

public class ConfiguracionBotDto
{
    public string Simbolo { get; set; } = "AAPL";
    public decimal Capital { get; set; } = 5;
    public decimal StopLoss { get; set; } = 2;
    public string Modo { get; set; } = "moderado";
    public int IntervaloMinutos { get; set; } = 5;
}
