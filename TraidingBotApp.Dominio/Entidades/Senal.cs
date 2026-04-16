using TraidingBotApp.Dominio;

namespace TraidingBotApp.Dominio.Entidades;

public class Senal
{
    public string Simbolo { get; set; } = "";
    public string Signal { get; set; } = "MANTENER";

    //Constructor
    public Senal(string simbolo, string signal)
    {
        Simbolo = simbolo;
        Signal = signal;
    }
}
