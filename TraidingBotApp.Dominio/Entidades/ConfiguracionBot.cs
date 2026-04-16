namespace TraidingBotApp.Dominio.Entidades;

public class ConfiguracionBot
{
    public string Simbolo { get; set; } = "AAPL";
    public decimal Capital { get; set; } = 5;
    public decimal StopLoss { get; set; } = 2;
    public string Modo { get; set; } = "moderado";
    public int IntervaloMinutos { get; set; } = 5;

    //Contructor para inicializar el estado del bot
    public ConfiguracionBot(string simbolo, decimal capital, decimal stopLoss, string modo, int intervaloMinutos)
    {
        Simbolo = simbolo;
        Capital = capital;
        StopLoss = stopLoss;
        Modo = modo;
        IntervaloMinutos = intervaloMinutos;
    }
}
