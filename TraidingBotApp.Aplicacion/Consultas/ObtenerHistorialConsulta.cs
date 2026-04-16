using MediatR;
using TraidingBotApp.Aplicacion.DTOs;
using TraidingBotApp.Dominio.Results;

namespace TraidingBotApp.Aplicacion.Consultas;

public record ObtenerHistorialConsulta(string simbolo)
    : IRequest<Resultado<List<PrecioGraficaDto>>> { }
