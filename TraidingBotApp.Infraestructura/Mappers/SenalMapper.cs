using TraidingBotApp.Dominio.Entidades;
using TraidingBotApp.Infraestructura.DTOs;

namespace TraidingBotApp.Infraestructura.Mappers;

public static class SenalMapper
{
    public static Senal ToDomain(RespuestaSenalDto respuestaSenal) =>
        new Senal(respuestaSenal.Symbol, respuestaSenal.Signal);
}
