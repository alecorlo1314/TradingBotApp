using MediatR;
using TraidingBotApp.Aplicacion.DTOs;
using TraidingBotApp.Dominio.Interfaces;
using TraidingBotApp.Dominio.Results;

namespace TraidingBotApp.Aplicacion.Consultas.Manejadores;

public class ObtenerIndicadoresManejador(ITradingApiServicio tradingApiServicio)
    : IRequestHandler<ObtenerIndicadoresConsulta, Resultado<IndicadoresDto>>
{
    public async Task<Resultado<IndicadoresDto>> Handle(ObtenerIndicadoresConsulta request, CancellationToken cancellationToken)
    {
        var resultado = await tradingApiServicio.ObtenerIndicadoresAsync(request.simbolo, cancellationToken);

        if (!resultado.FueExitoso)
            return resultado.Error!;

        return resultado.Map(i =>
        new IndicadoresDto
        {
            Simbolo = i.Symbol,
            Rsi = i.Rsi,
            Macd = i.Macd,
            MacdSenal = i.MacdSignal,
            MacdHist = i.MacdHist,
            Cierre = i.Close
        });
    }
}
