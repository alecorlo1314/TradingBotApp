namespace TraidingBotApp.Dominio.Entidades;

public class EstadoBot
{
    public bool Ejecutando { get; set; }
    public string? Simbolo { get; set; }
    public decimal Capital { get; set; }
    public string? Modo { get; set; }

    //Contructor para inicializar el estado del bot
    public EstadoBot(bool ejecutando, string? simbolo, decimal capital, string? modo)
    {
        Ejecutando = ejecutando;
        Simbolo = simbolo;
        Capital = capital;
        Modo = modo;
    }
}
