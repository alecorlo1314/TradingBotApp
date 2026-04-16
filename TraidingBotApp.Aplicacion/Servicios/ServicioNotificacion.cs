using Plugin.Firebase.CloudMessaging;
using TraidingBotApp.Dominio.Interfaces;

namespace TraidingBotApp.Aplicacion.Servicios;

/// <summary>
/// Obtiene el token FCM del celular y lo registra en la API. 
/// </summary>
public class ServicioNotificacion(ITradingApiServicio tradingApiServicio)
{
    public async Task RegisterDeviceAsync()
    {
		try
		{
			await CrossFirebaseCloudMessaging.Current.CheckIfValidAsync();
			var token = await CrossFirebaseCloudMessaging.Current.GetTokenAsync();

            if (!string.IsNullOrEmpty(token))
            {
                await tradingApiServicio.RegistrarTokenDispositivoAsync(token);
                Console.WriteLine($"[FCM] Token registrado: {token[..20]}...");
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"[FCM] Error registrando token: {ex.Message}");
        }
    }
}