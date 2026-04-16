using MediatR;
using TraidingBotApp.Aplicacion.DTOs;
using TraidingBotApp.Dominio.Interfaces;
using TraidingBotApp.Dominio.Results;

namespace TraidingBotApp.Aplicacion.Consultas.Manejadores;

public class ObtenerEstadoBotManejador(ITradingApiServicio tradingApiServicio)
    : IRequestHandler<ObtenerEstadoBotConsulta, Resultado<EstadoBotDto>?>
{
    public async Task<Resultado<EstadoBotDto>?> Handle(ObtenerEstadoBotConsulta request, CancellationToken cancellationToken)
    {
        var resultado = await tradingApiServicio.ObtenerEstadoBotAsync(cancellationToken);

        return resultado.Map(x =>
        {
            if (x is null)
                return new EstadoBotDto();

            return new EstadoBotDto
            {
                Ejecutando = x.Ejecutando,
                Simbolo = x.Simbolo,
                Capital = x.Capital,
                Modo = x.Modo
            };
        });
    }
}