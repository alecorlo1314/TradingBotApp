using TraidingBotApp.Dominio.Entidades;
using TraidingBotApp.Infraestructura.DTOs;

namespace TraidingBotApp.Infraestructura.Mappers;

public static class HistorialMapper
{
    public static RespuestaHitorialDto ToDto(this List<PuntoPrecio> puntos, string simbolo, string periodo)
    {
        return new RespuestaHitorialDto
        {
            Symbol = simbolo,
            Periodo = periodo,
            Data = puntos.Select(p => new PuntoPrecioDto
            {
                Date = p.Fecha,
                Close = p.Cierre
            }).ToList()
        };
    }
}
