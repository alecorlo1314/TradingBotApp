using MediatR;
using TraidingBotApp.Dominio.Results;

namespace TraidingBotApp.Aplicacion.Comandos;

public record DetenerBotComando() 
    : IRequest<Resultado<bool>> { }
