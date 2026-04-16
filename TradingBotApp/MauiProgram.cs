using Polly;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.LifecycleEvents;
using Syncfusion.Maui.Core.Hosting;
using TradingBotApp.ViewModels;
using TradingBotApp.Vistas;
using TraidingBotApp.Aplicacion.Consultas;
using TraidingBotApp.Aplicacion.Servicios;
using TraidingBotApp.Dominio.Interfaces;
using TraidingBotApp.Infraestructura.Servicios;
#if ANDROID
using Android.OS;
#endif
using Microsoft.Maui;
#if ANDROID
using Plugin.Firebase.Core.Platforms.Android;
#endif

namespace TradingBotApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .RegisterFirebaseServices()
                .ConfigureSyncfusionCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Poppins-SemiBold.ttf", "PoppinsSemiBold");
                    fonts.AddFont("Inter-Medium.ttf", "InterMedium");
                    fonts.AddFont("Inter-SemiBold.ttf", "InterSemiBold");
                    fonts.AddFont("Inter-Regular.ttf", "InterRegular");
                });

            //Regitro HttpClient con Polly retry (reintentos con backoff exponencial)
            builder.Services.AddHttpClient<ITradingApiServicio, TradingApiServicio>(cliente =>
                {
                    cliente.BaseAddress = new Uri("https://trading-bot-api-production-1d10.up.railway.app/");
                    cliente.Timeout = TimeSpan.FromSeconds(15);
                })
                // Inyecta header X-API-Key en cada petición
                .AddHttpMessageHandler(() => new ApiKeyDelegatingHandler("Alekey1314"))
                //Regitro HttpClient con Polly retry (reintentos con backoff exponencial)
                .AddTransientHttpErrorPolicy(policy =>
                    policy.WaitAndRetryAsync(new[]
                    {
                        TimeSpan.FromSeconds(1),
                        TimeSpan.FromSeconds(2),
                        TimeSpan.FromSeconds(4),
                    }));

            // Registrar MediatR indicando el assembly donde están tus Manejadores
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<ObtenerBalanceConsulta>();
                cfg.RegisterServicesFromAssemblyContaining<ObtenerEstadoBotConsulta>();
                cfg.RegisterServicesFromAssemblyContaining<ObtenerPosicionConsulta>();
                cfg.RegisterServicesFromAssemblyContaining<ObtenerSenalConsulta>();
                cfg.RegisterServicesFromAssemblyContaining<ObtenerHistorialConsulta>();
                cfg.RegisterServicesFromAssemblyContaining<ObtenerIndicadoresConsulta>();
            });

            //Registro de Servicios
            builder.Services.AddSingleton<ServicioNotificacion>();

            //Registro de ViewModels (Singleton porque viven toda la app en tabs de Shell)
            builder.Services.AddSingleton<DashboardViewModel>();
            builder.Services.AddSingleton<BotConfigViewModel>();
            builder.Services.AddSingleton<OperacionesViewModel>();

            //Registro de Vistas
            builder.Services.AddSingleton<DashboardPage>();
            builder.Services.AddSingleton<BotConfigPage>();
            builder.Services.AddSingleton<OperacionesPage>();

            //Ajustar el Underline y el cursor de los entries (SOLO ANDROID)
            builder.ConfigureMauiHandlers(handlers =>
            {
                EntryHandler.Mapper.AppendToMapping("UnderlineAndCursorColor", (handler, view) =>
                {
#if ANDROID
                    if(handler.PlatformView is Android.Widget.EditText editText) 
                    {
                        var color = Android.Graphics.Color.ParseColor("#BACBBF"); 

                        // Cambiar color del underline
                        editText.BackgroundTintList =
                                Android.Content.Res.ColorStateList.ValueOf(
        Android.Graphics.Color.Transparent);

                        // Cambiar color del cursor
                        if (Build.VERSION.SdkInt >= BuildVersionCodes.Q)
                        {
                            editText.TextCursorDrawable.SetTint(color);
                        }
                    }
#endif
                });
            });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static MauiAppBuilder RegisterFirebaseServices(this MauiAppBuilder builder)
        {
            builder.ConfigureLifecycleEvents(events =>
            {
#if ANDROID
        events.AddAndroid(android => android.OnCreate((activity, _) =>
        {
            CrossFirebase.Initialize(
                activity,
                () => Platform.CurrentActivity!
            );
        }));
#endif
            });
            return builder;
        }
    }
}
