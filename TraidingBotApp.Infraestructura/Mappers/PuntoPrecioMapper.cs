using TraidingBotApp.Dominio.Entidades;
using TraidingBotApp.Infraestructura.DTOs;

namespace TraidingBotApp.Infraestructura.Mappers;

public static class PuntoPrecioMapper
{
    public static PuntoPrecio toEntidad(this  PuntoPrecioDto puntoPrecioDto)
    {
        return new PuntoPrecio(
            fecha: puntoPrecioDto.Date,
            cierre: puntoPrecioDto.Close
        );
    }

    public static List<PuntoPrecio> toEntidadList(this List<PuntoPrecioDto> dtos)
    {
        return dtos.Select(d => d.toEntidad()).ToList();
    }
}
