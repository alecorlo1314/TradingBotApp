using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using TraidingBotApp.Aplicacion.Comandos;
using TraidingBotApp.Aplicacion.Consultas;
using TraidingBotApp.Aplicacion.DTOs;

namespace TradingBotApp.ViewModels;

public partial class BotConfigViewModel(IMediator mediator) : ViewModelBase
{
    #region Configuracion del Bot
    [ObservableProperty] private string _simbolo = "AAPL";
    [ObservableProperty] private decimal _capital = 5;
    [ObservableProperty] private decimal _stopLoss = 2;
    [ObservableProperty] private int _intervaloMinutos = 5;
    [ObservableProperty] private int _indiceModo = 1; // 0=conservative 1=moderate 2=aggressive

    [ObservableProperty] private bool _botCorriendo;
    [ObservableProperty] private string _estadoMensaje = "";

    public List<string> Modos => ["Conservador", "Moderado", "Agresivo"];

    private string ModoSeleccionado => IndiceModo switch
    {
        0 => "conservative",
        2 => "aggressive",
        _ => "moderate",
    };

    #endregion

    #region Caché de estado del bot
    private DateTimeOffset? _ultimaCargaEstado;
    private static readonly TimeSpan CacheTtlEstado = TimeSpan.FromSeconds(30);

    #endregion

    #region Comando Cargar Estatus Async
    [RelayCommand]
    public async Task CargarEstatusAsync()
    {
        // Si el estado es reciente (menos de 30s), evitar llamada redundante
        if (_ultimaCargaEstado.HasValue && DateTimeOffset.Now - _ultimaCargaEstado.Value < CacheTtlEstado)
            return;

        await EjecutarConCargaAsync(async () =>
        {
            var resultado = await mediator.Send(new ObtenerEstadoBotConsulta());

            if (resultado is null || !resultado.FueExitoso)
            {
                MostrarError(resultado?.Error?.Descripcion ?? "Error desconocido");
                await MostrarToastAsync(resultado?.Error?.Descripcion ?? "Error inesperado");
                return;
            }

            var estado = resultado.Valor;

            BotCorriendo = estado.Ejecutando;

            if (estado.Ejecutando)
            {
                Simbolo = estado.Simbolo ?? Simbolo;
                Capital = estado.Capital;
                EstadoMensaje = $"Bot activo — {estado.Simbolo} | Modo: {estado.Modo}";
            }
            else
            {
                EstadoMensaje = "Bot detenido";
            }

            // Actualizar timestamp de caché al finalizar con éxito
            _ultimaCargaEstado = DateTimeOffset.Now;
        });
    }

    #endregion

    #region Comando Iniciar Bot Async
    [RelayCommand]
    public async Task IniciarBotAsync()
    {
        if (string.IsNullOrWhiteSpace(Simbolo))
        {
            EstadoMensaje = "⚠️ Ingresa un símbolo válido (ej: AAPL)";
            return;
        }

        await EjecutarConCargaAsync(async () =>
        {
            EstadoMensaje = "Iniciando bot...";

            var config = new ConfiguracionBotDto
            {
                Simbolo = Simbolo,
                Capital = Capital,
                StopLoss = StopLoss,
                Modo = ModoSeleccionado,
                IntervaloMinutos = IntervaloMinutos
            };

            var resultado = await mediator.Send(new IniciarBotComando(config));

            if (resultado is null || !resultado.FueExitoso)
            {
                MostrarError(resultado?.Error?.Descripcion ?? "Sin conexión con el servidor");
                await MostrarToastAsync("No se pudo conectar con la API");
                return;
            }

            BotCorriendo = true;
            EstadoMensaje = $"✅ Bot iniciado — {config.Simbolo} | ${config.Capital} | cada {config.IntervaloMinutos} min";
        });
    }

    #endregion

    #region Comando Detener Bot Async
    [RelayCommand]
    public async Task DetenerBotAsync()
    {
        await EjecutarConCargaAsync(async () =>
        {
            EstadoMensaje = "Deteniendo bot...";

            var resultado = await mediator.Send(new DetenerBotComando());

            if (resultado is null || !resultado.FueExitoso)
            {
                MostrarError(resultado?.Error?.Descripcion ?? "Sin conexión con el servidor");
                await MostrarToastAsync("No se pudo conectar con la API");
                return;
            }

            BotCorriendo = false;
            EstadoMensaje = "🛑 Bot detenido";
        });
    }

    #endregion
}

