using MediatR;
using TraidingBotApp.Aplicacion.DTOs;
using TraidingBotApp.Dominio.Results;

namespace TraidingBotApp.Aplicacion.Consultas;

public record ObtenerPosicionConsulta 
    : IRequest<Resultado<List<PosicionDto>>> { }
