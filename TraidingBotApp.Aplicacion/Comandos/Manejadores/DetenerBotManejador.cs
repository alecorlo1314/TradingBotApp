using MediatR;
using TraidingBotApp.Dominio.Interfaces;
using TraidingBotApp.Dominio.Results;

namespace TraidingBotApp.Aplicacion.Comandos.Manejadores;

public class DetenerBotManejador(ITradingApiServicio tradingApiServicio)
    : IRequestHandler<DetenerBotComando, Resultado<bool>>
{
    public async Task<Resultado<bool>> Handle(DetenerBotComando request, CancellationToken cancellationToken)
    {
        return await tradingApiServicio.DetenerBotAsync(cancellationToken);
    }
}
