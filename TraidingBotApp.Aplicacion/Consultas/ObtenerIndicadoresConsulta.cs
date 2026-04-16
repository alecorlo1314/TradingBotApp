using MediatR;
using TraidingBotApp.Aplicacion.DTOs;
using TraidingBotApp.Dominio.Results;

namespace TraidingBotApp.Aplicacion.Consultas;

public record ObtenerIndicadoresConsulta(string simbolo)
    : IRequest<Resultado<IndicadoresDto>> { }
