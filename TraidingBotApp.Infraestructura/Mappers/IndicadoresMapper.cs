using TraidingBotApp.Dominio.Entidades;
using TraidingBotApp.Infraestructura.DTOs;

namespace TraidingBotApp.Infraestructura.Mappers;

public static class IndicadoresMapper
{
    public static Indicadores ToDomain(RespuestaIndicadorDto dto)
    {
        return new Indicadores(dto.Symbol, dto.Rsi, dto.Macd, dto.MacdSignal, dto.MacdHist, dto.Close);
    }
}
