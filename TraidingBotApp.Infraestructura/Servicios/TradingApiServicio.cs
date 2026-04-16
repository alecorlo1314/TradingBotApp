using System.Net.Http.Json;
using System.Text.Json;
using TraidingBotApp.Dominio.Entidades;
using TraidingBotApp.Dominio.Interfaces;
using TraidingBotApp.Dominio.Results;
using TraidingBotApp.Infraestructura.DTOs;
using TraidingBotApp.Infraestructura.Mappers;

namespace TraidingBotApp.Infraestructura.Servicios;

public class TradingApiServicio : ITradingApiServicio
{
    #region Dependencias
    private readonly HttpClient _httpClient;

    #endregion

    #region configuracion JSON
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    #endregion

    #region Constructor
    public TradingApiServicio(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    #endregion

    #region Obtener Balance
    public async Task<Resultado<Balance>> ObtenerBalanceAsync(CancellationToken ct = default)
    {
        try
        {
            var balancedto = await _httpClient.GetFromJsonAsync<RespuestaBalanceDto>(
                "api/account/balance", JsonOptions, ct);

            if (balancedto == null)
                return Errores.AccountNotFound;

            return BalanceMapper.ToDomain(balancedto);
        }
        catch (HttpRequestException)
        {
            return Errores.ErrorRed;
        }
        catch (TaskCanceledException)
        {
            return Errores.Timeout;
        }
        catch (JsonException)
        {
            return Errores.ErrorDeserializacion;
        }
        catch (Exception)
        {
            return Errores.Unexpected;
        }
    }
    #endregion

    #region Obtener Posiciones
    public async Task<Resultado<List<Posicion>>> ObtenerPosicionesAsync(CancellationToken ct = default)
    {
        try
        {
            var respuesta = await _httpClient.GetFromJsonAsync<RespuestaPosicionesDto>(
                "api/account/positions", JsonOptions, ct);

            if (respuesta == null)
                return Errores.NotFound;

            var posiciones = PosicionesMapper.ListToDominio(respuesta);

            return posiciones ?? [];
        }
        catch (HttpRequestException)
        {
            return Errores.ErrorRed;
        }
        catch (TaskCanceledException)
        {
            return Errores.Timeout;
        }
        catch (JsonException)
        {
            return Errores.ErrorDeserializacion;
        }
        catch (Exception)
        {
            return Errores.Unexpected;
        }
    }
    #endregion

    #region Obtener Estado Bot
    public async Task<Resultado<EstadoBot>> ObtenerEstadoBotAsync(CancellationToken ct = default)
    {
        try
        {
            var respuesta = await _httpClient
                .GetFromJsonAsync<EstadoBotRespuestaDto>("api/bot/status", JsonOptions, ct);

            if (respuesta == null)
                return Errores.NotFound;

            return EstadoBotMapper.ToDomain(respuesta);
        }
        catch (HttpRequestException)
        {
            return Errores.ErrorRed;
        }
        catch (TaskCanceledException)
        {
            return Errores.Timeout;
        }
        catch (JsonException)
        {
            return Errores.ErrorDeserializacion;
        }
        catch (Exception)
        {
            return Errores.Unexpected;
        }
    }
    #endregion

    #region Iniciar Bot
    public async Task<Resultado<bool>> IniciarBotAsync(ConfiguracionBot config, CancellationToken ct = default)
    {
        try
        {
            var configDto = ConfiguracionBotMapper.ToDto(config);

            var respuesta = await _httpClient.PostAsJsonAsync("api/bot/start", configDto, ct);

            if (!respuesta.IsSuccessStatusCode)
                return MapHttpError(respuesta.StatusCode);

            return true;
        }
        catch (HttpRequestException)
        {
            return Errores.ErrorRed;
        }
        catch (TaskCanceledException)
        {
            return Errores.Timeout;
        }
        catch (Exception)
        {
            return Errores.Unexpected;
        }
    }
    #endregion

    #region Detener Bot
    public async Task<Resultado<bool>> DetenerBotAsync(CancellationToken ct = default)
    {
        try
        {
            var respuesta = await _httpClient.PostAsync("api/bot/stop", null, ct);

            if (!respuesta.IsSuccessStatusCode)
                return MapHttpError(respuesta.StatusCode);

            return true;
        }
        catch (HttpRequestException)
        {
            return Errores.ErrorRed;
        }
        catch (TaskCanceledException)
        {
            return Errores.Timeout;
        }
        catch (Exception)
        {
            return Errores.Unexpected;
        }
    }
    #endregion

    #region Obtener Senal
    public async Task<Resultado<Senal>> ObtenerSenalAsync(string simbolo, CancellationToken ct = default)
    {
        try
        {
            var respuesta = await _httpClient.GetFromJsonAsync<RespuestaSenalDto>(
                $"api/market/signal/{simbolo}", JsonOptions, ct);

            if (respuesta == null)
                return Errores.NotFound;

            return SenalMapper.ToDomain(respuesta);
        }
        catch (HttpRequestException)
        {
            return Errores.ErrorRed;
        }
        catch (TaskCanceledException)
        {
            return Errores.Timeout;
        }
        catch (JsonException)
        {
            return Errores.ErrorDeserializacion;
        }
        catch (Exception)
        {
            return Errores.Unexpected;
        }
    }
    #endregion

    #region Obtener Historial de Precio
    public async Task<Resultado<List<PuntoPrecio>>> ObtenerHistorialPreciosAsync(string simbolo, string periodo = "3mo", CancellationToken ct = default)
    {
        try
        {
            var respuesta = await _httpClient.GetFromJsonAsync<RespuestaHitorialDto>(
                $"api/market/history/{simbolo}?period={periodo}", JsonOptions, ct);

            if (respuesta == null || respuesta.Data == null || respuesta.Data.Count == 0)
                return Errores.NotFound;

            var historial = respuesta.Data.toEntidadList();
            return historial;
        }
        catch (HttpRequestException)
        {
            return Errores.ErrorRed;
        }
        catch (TaskCanceledException)
        {
            return Errores.Timeout;
        }
        catch (JsonException)
        {
            return Errores.ErrorDeserializacion;
        }
        catch (Exception)
        {
            return Errores.Unexpected;
        }
    }
    #endregion

    #region Obtener Operaciones
    public async Task<Resultado<List<Operacion>>> ObtenerOperacionesAsync(int limite = 20, CancellationToken ct = default)
    {
        try
        {
            var respuesta = await _httpClient.GetFromJsonAsync<RespuestaOperacionDto>(
                $"api/trades/history?limit={limite}", JsonOptions, ct);

            if (respuesta == null)
                return Errores.NotFound;

            if (respuesta.Trades == null || respuesta.Trades.Count == 0)
                return Errores.NotFound;

            var operaciones = respuesta.Trades.ToEntityList();
            return operaciones;
        }
        catch (HttpRequestException)
        {
            return Errores.ErrorRed;
        }
        catch (TaskCanceledException)
        {
            return Errores.Timeout;
        }
        catch (JsonException)
        {
            return Errores.ErrorDeserializacion;
        }
        catch (Exception)
        {
            return Errores.Unexpected;
        }
    }
    #endregion

    #region Obtener Indicadores
    public async Task<Resultado<Indicadores>> ObtenerIndicadoresAsync(string simbolo, CancellationToken ct = default)
    {
        try
        {
            var respuesta = await _httpClient.GetFromJsonAsync<RespuestaIndicadorDto>(
                $"api/market/indicators/{simbolo}", JsonOptions, ct);

            if (respuesta == null)
                return Errores.NotFound;

            return IndicadoresMapper.ToDomain(respuesta);
        }
        catch (HttpRequestException) { return Errores.ErrorRed; }
        catch (TaskCanceledException) { return Errores.Timeout; }
        catch (JsonException) { return Errores.ErrorDeserializacion; }
        catch (Exception) { return Errores.Unexpected; }
    }

    #endregion

    #region Obtener Grafica
    public async Task<Resultado<List<PrecioGrafica>>> ObtenerHistorialGraficaAsync(string simbolo, CancellationToken ct = default)
    {
        try
        {
            var respuesta = await _httpClient.GetFromJsonAsync<RespuestaHitorialDto>(
                $"api/market/history/{simbolo}?period=3mo", JsonOptions, ct);

            if (respuesta == null || respuesta.Data == null)
                return Errores.NotFound;

            return respuesta.Data
                .Select(p => new PrecioGrafica(p.Date,p.Close))
                .ToList();
        }
        catch (HttpRequestException) { return Errores.ErrorRed; }
        catch (TaskCanceledException) { return Errores.Timeout; }
        catch (Exception) { return Errores.Unexpected; }
    }

    #endregion

    #region Registrar Dispositivo
    public async Task RegistrarTokenDispositivoAsync(string token, CancellationToken ct = default)
    {
        try
        {
            var payload = new { token };
            await _httpClient.PostAsJsonAsync("api/bot/register-token", payload, ct);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[API] RegisterDeviceToken error: {ex.Message}");
        }
    }
    #endregion

    #region Helpers
    private static Error MapHttpError(System.Net.HttpStatusCode statusCode)
    {
        return statusCode switch
        {
            System.Net.HttpStatusCode.BadRequest => Errores.BadRequest,
            System.Net.HttpStatusCode.Unauthorized => Errores.Unauthorized,
            System.Net.HttpStatusCode.Forbidden => Errores.Forbidden,
            System.Net.HttpStatusCode.NotFound => Errores.NotFound,
            System.Net.HttpStatusCode.Conflict => Errores.Conflict,
            (System.Net.HttpStatusCode)429 => Errores.RateLimit,
            System.Net.HttpStatusCode.InternalServerError => Errores.ServerError,
            System.Net.HttpStatusCode.ServiceUnavailable => Errores.ServiceUnavailable,
            _ => Errores.HttpNonSuccess
        };
    }

    #endregion
}
