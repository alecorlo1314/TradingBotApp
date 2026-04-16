using MediatR;
using TraidingBotApp.Aplicacion.DTOs;
using TraidingBotApp.Dominio.Results;

namespace TraidingBotApp.Aplicacion.Consultas;
public class ObtenerEstadoBotConsulta() 
    : IRequest<Resultado<EstadoBotDto>?> { }
