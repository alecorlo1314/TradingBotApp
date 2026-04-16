using TraidingBotApp.Dominio.Entidades;
using TraidingBotApp.Infraestructura.DTOs;

namespace TraidingBotApp.Infraestructura.Mappers;

public static class OperacionMapper
{
    private static string MapSideToLado(string side)
    {
        if (string.IsNullOrWhiteSpace(side)) return "";
        var s = side.Trim().ToLower();
        return s switch
        {
            "buy" => "comprar",
            "sell" => "vender",
            "b" => "comprar",
            "s" => "vender",
            _ => side
        };
    }

    public static Operacion ToEntity(this OperacionDto dto)
    {
        return new Operacion
        {
            Id = dto.Id ?? "",
            Simbolo = dto.Symbol ?? "",
            Lado = MapSideToLado(dto.Side),
            CantidadEjecutada = dto.FilledQty,
            PrecioPromedioEjecutado = dto.FilledAvg,
            Estado = dto.Status ?? "",
            CreadoEn = dto.CreatedAt ?? ""
        };
    }

    public static List<Operacion> ToEntityList(this IEnumerable<OperacionDto> dtos)
    {
        if (dtos == null) return new List<Operacion>();
        return dtos.Select(d => d.ToEntity()).ToList();
    }
}
