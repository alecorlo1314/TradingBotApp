using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using System.Collections.ObjectModel;
using TraidingBotApp.Aplicacion.Consultas;
using TraidingBotApp.Aplicacion.DTOs;
using TraidingBotApp.Dominio;

namespace TradingBotApp.ViewModels;

public partial class DashboardViewModel(IMediator mediator) : ViewModelBase
{
    #region Propiedades Observables
    [ObservableProperty] private decimal _efectivo;
    [ObservableProperty] private decimal _valorPortafolio;
    [ObservableProperty] private decimal _poderCompra;
    [ObservableProperty] private string _senal = "HOLD";
    [ObservableProperty] private Color _senalColor = AppColores.Gris;
    [ObservableProperty] private Color _senalBackGroundColor = AppColores.GrisBackGroundColor;
    [ObservableProperty] private bool _botCorriendo;
    [ObservableProperty] private string _botSimbolo = "–";
    [ObservableProperty] private string? _modo;
    [ObservableProperty]
    private ObservableCollection<PosicionDto> _posiciones = new();
    [ObservableProperty] private IndicadoresDto? _indicadores;
    [ObservableProperty] private List<PrecioGraficaDto> _historialPrecios = [];
    [ObservableProperty] private bool _tieneGrafica;

    #endregion

    #region Caché de respuestas
    private DateTimeOffset? _ultimaCarga;
    private static readonly TimeSpan CacheTtl = TimeSpan.FromSeconds(30);

    #endregion

    #region Comando Cargar Async
    [RelayCommand]
    public async Task CargarAsync()
    {
        // Si los datos son recientes (menos de 30s), evitar llamada redundante
        if (_ultimaCarga.HasValue && DateTimeOffset.Now - _ultimaCarga.Value < CacheTtl)
            return;

        await EjecutarConCargaAsync(async () =>
        {

            // Paso 1 — cargar balance, posiciones y estado del bot en paralelo
            var balanceTask = mediator.Send(new ObtenerBalanceConsulta());
            var posicionesTask = mediator.Send(new ObtenerPosicionConsulta());
            var botTask = mediator.Send(new ObtenerEstadoBotConsulta());

            await Task.WhenAll(balanceTask, posicionesTask, botTask);


            // Paso 2 — aplicar resultad
            var balanceResult = balanceTask.Result;
            var posicionesResult = posicionesTask.Result;
            var botResult = botTask.Result;

            if (!balanceResult.FueExitoso)
            {
                MostrarError(balanceResult.Error!.Descripcion);
                await MostrarToastAsync(balanceResult.Error.Descripcion);
                return;
            }

            if (!posicionesResult.FueExitoso)
            {
                MostrarError(posicionesResult.Error!.Descripcion);
                await MostrarToastAsync(posicionesResult.Error.Descripcion);
                return;
            }

            if (!botResult!.FueExitoso)
            {
                MostrarError(botResult.Error!.Descripcion);
                await MostrarToastAsync(botResult.Error.Descripcion);
                return;
            }

            var balance = balanceResult.Valor;
            var posiciones = posicionesResult.Valor;
            var bot = botResult.Valor;

            Efectivo = balance.Efectivo;
            ValorPortafolio = balance.ValorPortafolio;
            PoderCompra = balance.PoderCompra;

            Posiciones.Clear();

            foreach (var p in posiciones ?? [])
            {
                Posiciones.Add(p);
            }

            BotCorriendo = bot.Ejecutando;
            BotSimbolo = bot.Simbolo ?? "–";
            Modo = bot.Modo;

            if (bot.Ejecutando && bot.Simbolo is not null)
            {
                var senalResult = await mediator.Send(new ObtenerSenalConsulta(bot.Simbolo));

                if (!senalResult.FueExitoso)
                {
                    await MostrarToastAsync(senalResult.Error!.Descripcion);
                    return;
                }

                var senal = senalResult.Valor;

                Senal = senal.Senal;
                SenalColor = senal.ColorSenal;
                SenalColor = senal.ColorSenalBackGroundColor;
            }

            // Paso 3 — cargar indicadores y gráfica con el símbolo del bot
            await CargarIndicadoresAsync(BotSimbolo ?? "AAPL");

            // Actualizar timestamp de caché al finalizar con éxito
            _ultimaCarga = DateTimeOffset.Now;
        });
    }

    #endregion

    #region Comando Cargar Indicadores
    [RelayCommand]
    public async Task CargarIndicadoresAsync(string simbolo)
    {
        // Si los datos son recientes (menos de 30s), evitar llamada redundante
        if (_ultimaCarga.HasValue && DateTimeOffset.Now - _ultimaCarga.Value < CacheTtl)
            return;

        await EjecutarConCargaAsync(async () =>
        {
            var indicadoresTask = mediator.Send(new ObtenerIndicadoresConsulta(simbolo));
            var historicoTask = mediator.Send(new ObtenerHistorialConsulta(simbolo));

            await Task.WhenAll(indicadoresTask, historicoTask);

            var indResult = await indicadoresTask;
            if (indResult?.FueExitoso == true)
                Indicadores = indResult.Valor;

            var histResult = await historicoTask;
            if (histResult?.FueExitoso == true)
            {
                HistorialPrecios = histResult.Valor ?? [];
                TieneGrafica = HistorialPrecios.Count > 0;
            }

            // Actualizar timestamp de caché al finalizar con éxito
            _ultimaCarga = DateTimeOffset.Now;
        });
    }

    #endregion
}
