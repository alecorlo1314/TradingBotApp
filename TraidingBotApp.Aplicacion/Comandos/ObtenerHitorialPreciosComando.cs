using MediatR;
using TraidingBotApp.Aplicacion.DTOs;
using TraidingBotApp.Dominio.Results;

namespace TraidingBotApp.Aplicacion.Comandos;

public record ObtenerHitorialPreciosComando(string simbolo, string periodo = "3mo")
    : IRequest<Resultado<List<PuntoPrecioDto>>> { }
