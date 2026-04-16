using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using System.Collections.ObjectModel;
using TraidingBotApp.Aplicacion.Comandos;
using TraidingBotApp.Aplicacion.DTOs;

namespace TradingBotApp.ViewModels;

public partial class OperacionesViewModel
    (IMediator mediator) : ViewModelBase
{
    #region Propiedades Observables
    [ObservableProperty] private ObservableCollection<OperacionDto> _operaciones = [];
    [ObservableProperty] private bool _estaVacio;

    #endregion

    #region Caché de operaciones
    private DateTimeOffset? _ultimaCarga;
    private static readonly TimeSpan CacheTtl = TimeSpan.FromSeconds(15);

    #endregion

    #region Comando Cargar Async
    [RelayCommand]
    public async Task CargarAsync()
    {
        // Si las operaciones son recientes (menos de 15s), evitar llamada redundante
        if (_ultimaCarga.HasValue && DateTimeOffset.Now - _ultimaCarga.Value < CacheTtl)
            return;

        await EjecutarConCargaAsync(async () =>
        {
            var resultado = await mediator.Send(new ObtenerOperacionesComando());

            if (!resultado.FueExitoso)
            {
                MostrarError(resultado.Error!.Descripcion);
                await MostrarToastAsync(resultado.Error.Descripcion);
                return;
            }

            Operaciones.Clear();
            foreach (var op in resultado.Valor)
            {
                Operaciones.Add(op);
            }
            EstaVacio = Operaciones.Count == 0;

            // Actualizar timestamp de caché al finalizar con éxito
            _ultimaCarga = DateTimeOffset.Now;
        });
    }
    #endregion
}

