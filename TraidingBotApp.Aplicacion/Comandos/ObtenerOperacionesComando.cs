using MediatR;
using TraidingBotApp.Aplicacion.DTOs;
using TraidingBotApp.Dominio.Results;

namespace TraidingBotApp.Aplicacion.Comandos;

public record ObtenerOperacionesComando(int limite = 20)
    : IRequest<Resultado<List<OperacionDto>>>;
