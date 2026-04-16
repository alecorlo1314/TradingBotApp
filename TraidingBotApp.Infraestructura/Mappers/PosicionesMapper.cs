using TraidingBotApp.Dominio.Entidades;
using TraidingBotApp.Infraestructura.DTOs;

namespace TraidingBotApp.Infraestructura.Mappers;

public static class PosicionesMapper
{
    public static Posicion ToDominio(PosicionDto posicionRespuestaDto) =>
        new Posicion(posicionRespuestaDto.Symbol, posicionRespuestaDto.Qty, posicionRespuestaDto.AvgEntry,
            posicionRespuestaDto.CurrentPrice, posicionRespuestaDto.Pnl, posicionRespuestaDto.PnlPct);

    public static List<Posicion>? ListToDominio(RespuestaPosicionesDto respuestaPosicionesDto) =>
        respuestaPosicionesDto.Posiciones?.Select(ToDominio).ToList();
}
