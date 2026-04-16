using MediatR;
using TraidingBotApp.Aplicacion.DTOs;
using TraidingBotApp.Dominio.Interfaces;
using TraidingBotApp.Dominio.Results;

namespace TraidingBotApp.Aplicacion.Consultas.Manejadores;

public class ObtenerSenalManejador(ITradingApiServicio tradingApiServicio)
    : IRequestHandler<ObtenerSenalConsulta, Resultado<SenalDto>>
{
    public async Task<Resultado<SenalDto>> Handle(ObtenerSenalConsulta request, CancellationToken cancellationToken)
    {
        var resultado = await tradingApiServicio.ObtenerSenalAsync(request.simbolo, cancellationToken);

        if (!resultado.FueExitoso)
            return resultado.Error!;

        var senal = resultado.Valor;

        var dto = new SenalDto
        {
            Simbolo = senal!.Simbolo,
            Senal = senal.Signal,
        };

        return dto;
    }
}