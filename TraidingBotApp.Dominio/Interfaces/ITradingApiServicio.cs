using TraidingBotApp.Dominio.Entidades;
using TraidingBotApp.Dominio.Results;

namespace TraidingBotApp.Dominio.Interfaces;

public interface ITradingApiServicio
{
    Task<Resultado<Balance>> ObtenerBalanceAsync(CancellationToken ct = default);
    Task<Resultado<List<Posicion>>> ObtenerPosicionesAsync(CancellationToken ct = default);
    Task<Resultado<EstadoBot>> ObtenerEstadoBotAsync(CancellationToken ct = default);
    Task<Resultado<bool>> IniciarBotAsync(ConfiguracionBot config, CancellationToken ct = default);
    Task<Resultado<bool>> DetenerBotAsync(CancellationToken ct = default);
    Task<Resultado<Senal>> ObtenerSenalAsync(string simbolo, CancellationToken ct = default);
    Task<Resultado<List<PuntoPrecio>>> ObtenerHistorialPreciosAsync(string simbolo, string periodo = "3mo", CancellationToken ct = default);
    Task<Resultado<List<Operacion>>> ObtenerOperacionesAsync(int limite = 20, CancellationToken ct = default);
    Task<Resultado<Indicadores>> ObtenerIndicadoresAsync(string simbolo, CancellationToken ct = default);
    Task<Resultado<List<PrecioGrafica>>> ObtenerHistorialGraficaAsync(string simbolo, CancellationToken ct = default);
    Task RegistrarTokenDispositivoAsync(string token, CancellationToken ct = default);
}
