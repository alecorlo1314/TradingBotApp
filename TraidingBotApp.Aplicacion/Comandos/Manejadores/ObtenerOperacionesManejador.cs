using MediatR;
using TraidingBotApp.Aplicacion.DTOs;
using TraidingBotApp.Dominio.Interfaces;
using TraidingBotApp.Dominio.Results;

namespace TraidingBotApp.Aplicacion.Comandos.Manejadores;

public class ObtenerOperacionesManejador(ITradingApiServicio tradingApiServicio)
    : IRequestHandler<ObtenerOperacionesComando, Resultado<List<OperacionDto>>>
{
    public async Task<Resultado<List<OperacionDto>>> Handle(ObtenerOperacionesComando request, CancellationToken cancellationToken)
    {
        var resultado = await tradingApiServicio.ObtenerOperacionesAsync(request.limite, cancellationToken);

        if (!resultado.FueExitoso)
            return resultado.Error!;

        var dto = resultado.Valor?.Select(o => new OperacionDto
        {
            Id = o.Id,
            Simbolo = o.Simbolo,
            Lado = o.Lado,
            CantidadEjecutada = o.PrecioPromedioEjecutado,
            PrecioPromedioEjecutado = o.PrecioPromedioEjecutado,
            Estado = o.Estado,
            CreadoEn = o.CreadoEn
        }).ToList();

        return dto;
    }
}