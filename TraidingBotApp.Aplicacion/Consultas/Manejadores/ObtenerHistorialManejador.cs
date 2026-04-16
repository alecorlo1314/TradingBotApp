using MediatR;
using TraidingBotApp.Aplicacion.DTOs;
using TraidingBotApp.Dominio.Interfaces;
using TraidingBotApp.Dominio.Results;

namespace TraidingBotApp.Aplicacion.Consultas.Manejadores;

public class ObtenerHistorialManejador(ITradingApiServicio tradingApiServicio)
    : IRequestHandler<ObtenerHistorialConsulta, Resultado<List<PrecioGraficaDto>>>
{
    public async Task<Resultado<List<PrecioGraficaDto>>> Handle(ObtenerHistorialConsulta request, CancellationToken cancellationToken)
    {
        var resultado = await tradingApiServicio.ObtenerHistorialGraficaAsync(request.simbolo, cancellationToken);

        if(!resultado.FueExitoso)
            return resultado.Error!;

        return resultado.Map(h => h
        .Select(p => new PrecioGraficaDto
        {
            Fecha = p.Date,
            Cierre = p.Close
        }).ToList());
    }
}
