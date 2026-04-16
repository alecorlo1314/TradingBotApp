using MediatR;
using TraidingBotApp.Aplicacion.DTOs;
using TraidingBotApp.Dominio.Interfaces;
using TraidingBotApp.Dominio.Results;

namespace TraidingBotApp.Aplicacion.Consultas.Manejadores;

public class ObtenerPosicionManejador(ITradingApiServicio tradingApiServicio)
    : IRequestHandler<ObtenerPosicionConsulta, Resultado<List<PosicionDto>>>
{
    public async Task<Resultado<List<PosicionDto>>> Handle(ObtenerPosicionConsulta request, CancellationToken cancellationToken)
    {
        var resultado = await tradingApiServicio.ObtenerPosicionesAsync(cancellationToken);

        if (resultado.EsError)
            return resultado.Error!;

        return resultado.Map(posiciones => posiciones
            .Select(p => new PosicionDto
            {
                Simbolo = p.Simbolo,
                Cantidad = p.Cantidad,
                PrecioEntradaPromedio = p.PrecioEntradaPromedio,
                PrecioActual = p.PrecioActual,
                GananciaPerdida = p.GananciaPerdida,
                GananciaPerdidaPct = p.GananciaPerdidaPct
            })
            .ToList());
    }
}