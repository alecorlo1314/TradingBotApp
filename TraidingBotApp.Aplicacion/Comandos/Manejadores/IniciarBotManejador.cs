using MediatR;
using TraidingBotApp.Dominio.Entidades;
using TraidingBotApp.Dominio.Interfaces;
using TraidingBotApp.Dominio.Results;

namespace TraidingBotApp.Aplicacion.Comandos.Manejadores;

public class IniciarBotManejador(ITradingApiServicio tradingApiServicio)
    : IRequestHandler<IniciarBotComando, Resultado<bool>>
{
    public async Task<Resultado<bool>> Handle(IniciarBotComando request, CancellationToken cancellationToken)
    {
        var configuracionBot = new ConfiguracionBot(
            request.configuracionBotDto.Simbolo,
            request.configuracionBotDto.Capital,
            request.configuracionBotDto.StopLoss,
            request.configuracionBotDto.Modo,
            request.configuracionBotDto.IntervaloMinutos);

        return await tradingApiServicio.IniciarBotAsync(configuracionBot, cancellationToken);
    }
}
