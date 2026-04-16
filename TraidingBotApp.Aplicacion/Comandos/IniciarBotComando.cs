using MediatR;
using TraidingBotApp.Aplicacion.DTOs;
using TraidingBotApp.Dominio.Results;

namespace TraidingBotApp.Aplicacion.Comandos;

public record IniciarBotComando(ConfiguracionBotDto configuracionBotDto)
    : IRequest<Resultado<bool>> { }
