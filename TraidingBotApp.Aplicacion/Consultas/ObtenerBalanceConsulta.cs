using MediatR;
using TraidingBotApp.Aplicacion.DTOs;
using TraidingBotApp.Dominio.Results;

namespace TraidingBotApp.Aplicacion.Consultas;

/// <summary>
/// Obtiene el balance actual
/// Retorna BalanceDto de lo contrario un null
/// </summary>
public record ObtenerBalanceConsulta()
    : IRequest<Resultado<BalanceDto>> { }
