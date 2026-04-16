using System.Globalization;
using TradingBotApp.Vistas;
using TraidingBotApp.Aplicacion.Servicios;

namespace TradingBotApp
{
    public partial class App : Application
    {
        private readonly ServicioNotificacion _notifications;
        private CancellationTokenSource? _startupCts;

        public App(ServicioNotificacion notifications)
        {
            InitializeComponent();

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1JHaF5cWWdCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdlWXxccnRUQmJeUkZyVkpWYEo=");

            //Cultura
            CultureInfo.CurrentUICulture = new CultureInfo("es-ES");

            _notifications = notifications;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            _startupCts = new CancellationTokenSource();
            _ = RegisterDeviceDelayedAsync(_startupCts.Token);
            return new Window(new AppShell());
        }

        private async Task RegisterDeviceDelayedAsync(CancellationToken ct)
        {
            try
            {
                await Task.Delay(3000, ct);
                await _notifications.RegisterDeviceAsync();
            }
            catch (OperationCanceledException)
            {
                // App se cerró o se backgroundeó antes de los 3s — no hacer nada
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FCM] Error al registrar: {ex.Message}");
            }
        }

        protected override void OnSleep()
        {
            // Cancelar el registro pendiente si la app se va a background
            _startupCts?.Cancel();
            _startupCts?.Dispose();
            _startupCts = null;
        }
    }
}
