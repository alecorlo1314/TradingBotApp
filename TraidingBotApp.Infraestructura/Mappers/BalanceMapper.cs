using TraidingBotApp.Dominio.Entidades;
using TraidingBotApp.Infraestructura.DTOs;

namespace TraidingBotApp.Infraestructura.Mappers;

public static class BalanceMapper
{
    /// <summary>
    /// Convierte BalanceRespuestaDto a Balance
    /// </summary>
    /// <param name="balanceRespuestaDto"></param>
    /// <returns></returns>
    public static Balance ToDomain(RespuestaBalanceDto balanceRespuestaDto) =>
        new Balance(balanceRespuestaDto.Cash, balanceRespuestaDto.PortfolioValue, balanceRespuestaDto.BuyingPower, balanceRespuestaDto.Currency);
}
