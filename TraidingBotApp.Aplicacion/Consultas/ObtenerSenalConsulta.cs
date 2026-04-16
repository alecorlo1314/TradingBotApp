using MediatR;
using TraidingBotApp.Aplicacion.DTOs;
using TraidingBotApp.Dominio.Results;

namespace TraidingBotApp.Aplicacion.Consultas;

public record ObtenerSenalConsulta(string simbolo)
    : IRequest<Resultado<SenalDto>> { }
