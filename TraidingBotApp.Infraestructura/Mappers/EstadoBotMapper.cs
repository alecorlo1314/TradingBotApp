using TraidingBotApp.Dominio.Entidades;
using TraidingBotApp.Infraestructura.DTOs;

namespace TraidingBotApp.Infraestructura.Mappers;

public static class EstadoBotMapper
{
    public static EstadoBot ToDomain(EstadoBotRespuestaDto estadoBotRespuestaDto) => 
        new EstadoBot(estadoBotRespuestaDto.Running, estadoBotRespuestaDto.Symbol,
            estadoBotRespuestaDto.Capital, estadoBotRespuestaDto.Mode);
}
