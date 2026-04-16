using MediatR;
using TraidingBotApp.Aplicacion.DTOs;
using TraidingBotApp.Dominio.Interfaces;
using TraidingBotApp.Dominio.Results;

namespace TraidingBotApp.Aplicacion.Consultas.Manejadores;

public class ObtenerBalanceManejador(ITradingApiServicio tradingApiServicio)
    : IRequestHandler<ObtenerBalanceConsulta, Resultado<BalanceDto>>
{
    public async Task<Resultado<BalanceDto>> Handle(ObtenerBalanceConsulta request, CancellationToken cancellationToken)
    {
        var resultado = await tradingApiServicio.ObtenerBalanceAsync(cancellationToken);

        return resultado.Map(b => new BalanceDto
        {
            Efectivo = b.Efectivo,
            ValorPortafolio = b.ValorPortafolio,
            PoderCompra = b.PoderComprar,
            Moneda = b.Moneda ?? "USD"
        });
    }
}
