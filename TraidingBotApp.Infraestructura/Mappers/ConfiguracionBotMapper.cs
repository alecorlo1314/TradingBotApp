using TraidingBotApp.Dominio.Entidades;
using TraidingBotApp.Infraestructura.DTOs;

namespace TraidingBotApp.Infraestructura.Mappers;

public static class ConfiguracionBotMapper
{
    public static ConfiguracionBotDto ToDto(this ConfiguracionBot configuracionBot)
    {
        return new ConfiguracionBotDto
        {
            Simbolo = configuracionBot.Simbolo,
            Capital = configuracionBot.Capital,
            StopLoss = configuracionBot.StopLoss,
            Modo = configuracionBot.Modo,
            IntervaloMinutos = configuracionBot.IntervaloMinutos
        };
    }
}
