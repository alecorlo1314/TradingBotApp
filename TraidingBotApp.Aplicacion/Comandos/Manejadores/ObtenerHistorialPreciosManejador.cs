using MediatR;
using TraidingBotApp.Aplicacion.DTOs;
using TraidingBotApp.Dominio.Interfaces;
using TraidingBotApp.Dominio.Results;

namespace TraidingBotApp.Aplicacion.Comandos.Manejadores;

public class ObtenerHistorialPreciosManejador(ITradingApiServicio tradingApiServicio)
    : IRequestHandler<ObtenerHitorialPreciosComando, Resultado<List<PuntoPrecioDto>>>
{
    public async Task<Resultado<List<PuntoPrecioDto>>> Handle(ObtenerHitorialPreciosComando request, CancellationToken cancellationToken)
    {
        var resultado = await tradingApiServicio
            .ObtenerHistorialPreciosAsync(request.simbolo, request.periodo, cancellationToken);

        if (!resultado.FueExitoso)
            return resultado.Error!;

        var dto = resultado.Valor
            .Select(h => new PuntoPrecioDto
            {
                Fecha = h.Fecha,
                Cierre = h.Cierre
            })
            .ToList();

        return dto;
    }
}