# 📱 TradingBotApp

> Aplicación móvil de trading algorítmico construida con **.NET MAUI**, conectada a un backend **FastAPI** en la nube que utiliza indicadores técnicos **RSI + MACD** y un modelo de redes neuronales **LSTM** para generar señales de compra y venta automáticas.

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![MAUI](https://img.shields.io/badge/MAUI-Android-3DDC84?style=flat-square&logo=android)](https://learn.microsoft.com/en-us/dotnet/maui/)
[![Platform](https://img.shields.io/badge/Platform-Android-brightgreen?style=flat-square)](https://developer.android.com/)
[![License](https://img.shields.io/badge/License-MIT-blue?style=flat-square)](LICENSE)

---

## 🧠 ¿Qué hace esta aplicación?

TradingBotApp es la **interfaz móvil** de un sistema de trading algorítmico completo. Permite al usuario configurar y controlar un bot que opera automáticamente en el mercado americano (NYSE/NASDAQ) mediante la API de [Alpaca Markets](https://alpaca.markets).

La app **no contiene lógica de trading** — esa responsabilidad recae en el [backend FastAPI](https://github.com/alecorlo1314/trading-bot-api) que corre 24/7 en la nube. MAUI actúa únicamente como interfaz de visualización y control.

---

## 🏗 Arquitectura del sistema

```
┌─────────────────────┐        HTTP REST        ┌──────────────────────────┐
│   TradingBotApp     │ ──────────────────────▶ │   FastAPI Backend        │
│   .NET MAUI         │        (JSON)            │   Railway (cloud)        │
│                     │                          │                          │
│  • Dashboard        │                          │  • RSI + MACD            │
│  • Bot Config       │                          │  • Modelo LSTM           │
│  • Historial        │                          │  • APScheduler           │
└─────────────────────┘                          │  • Stop-Loss automático  │
         ▲                                       └──────────────┬───────────┘
         │                                                      │
         │  Firebase FCM                              Alpaca SDK│
         │  (notificaciones push)                               ▼
         │                                       ┌──────────────────────────┐
         └───────────────────────────────────────│   Alpaca Markets         │
                                                 │   Paper / Live Trading   │
                                                 └──────────────────────────┘
```

---

## ✨ Funcionalidades

### 📊 Dashboard
- Balance de cuenta, efectivo y poder de compra en tiempo real
- Posiciones abiertas con P&L (ganancia/pérdida)
- Señal actual del bot: **BUY / SELL / HOLD**
- Indicador **RSI** con barra de progreso y descripción visual
- Indicador **MACD** con histograma y línea de señal
- Gráfica de precio histórico de los últimos 3 meses

### 🤖 Configuración del Bot
- Selección de símbolo (AAPL, TSLA, MSFT, BTC/USD, etc.)
- Capital por operación (slider $1 — $50)
- Stop-loss automático configurable
- Modo: Conservador / Moderado / Agresivo
- Intervalo de ejecución en minutos
- Inicio y detención del bot con un botón

### 📋 Historial de Operaciones
- Lista de las últimas 20 órdenes ejecutadas
- Badges visuales BUY (verde) / SELL (naranja)
- Precio de ejecución, cantidad y fecha

### 🔔 Notificaciones Push
- Alertas en tiempo real cuando el bot ejecuta una orden
- Notificación de stop-loss activado
- Funciona aunque la app esté cerrada (Firebase FCM v1)

---

## 🛠 Stack tecnológico

| Tecnología | Uso |
|---|---|
| **.NET MAUI 10** | Framework multiplataforma |
| **CommunityToolkit.Mvvm** | MVVM, ObservableProperty, RelayCommand |
| **MediatR** | Patrón CQRS para consultas y comandos |
| **Syncfusion MAUI** | Gráficas (SfCartesianChart) y controles UI |
| **Plugin.Firebase.CloudMessaging** | Notificaciones push FCM v1 |
| **Microsoft.Extensions.Http** | HttpClient con DI |

---

## 📁 Estructura del proyecto

```
TradingBotApp/
├── App.xaml / App.xaml.cs           # Entrada de la app, registro FCM
├── AppShell.xaml                    # Navegación por tabs
├── MauiProgram.cs                   # DI, Firebase, HttpClient
│
├── Dominio/
│   ├── Entidades/                   # Balance, Posicion, Senal, etc.
│   ├── Interfaces/                  # ITradingApiServicio
│   └── Results/                     # Resultado<T>, Error, Errores
│
├── Aplicacion/
│   ├── Consultas/                   # ObtenerBalance, ObtenerSenal, etc.
│   ├── Comandos/                    # IniciarBot, DetenerBot
│   ├── DTOs/                        # Objetos de transferencia de datos
│   └── Servicios/                   # ServicioNotificacion
│
├── Infraestructura/
│   ├── Servicios/                   # TradingApiServicio (HTTP)
│   ├── DTOs/                        # DTOs de respuesta de la API
│   └── Mappers/                     # Conversión DTO → Entidad
│
├── ViewModels/
│   ├── ViewModelBase.cs             # Carga, errores, toasts, navegación
│   ├── DashboardViewModel.cs
│   ├── BotConfigViewModel.cs
│   └── OperacionesViewModel.cs
│
├── Vistas/
│   ├── DashboardPage.xaml
│   ├── BotConfigPage.xaml
│   └── OperacionesPage.xaml
│
├── Converters/
│   └── Converters.cs                # InvertedBool, IsNotNull, DividirCien
│
├── Resources/
│   ├── Fonts/                       # Poppins, Inter
│   └── Styles/
│       ├── Colors.xaml
│       └── Styles.xaml
│
└── Platforms/
    └── Android/
        ├── MainActivity.cs          # Canal de notificaciones FCM
        ├── MainApplication.cs
        └── AndroidManifest.xml
```

---

## 🚀 Setup y configuración

### Prerrequisitos

- Visual Studio 2022 o superior
- .NET 10 SDK con workload MAUI instalado
- Android SDK (API 26+)
- Cuenta gratuita en [Alpaca Markets](https://alpaca.markets)
- Proyecto en [Firebase Console](https://console.firebase.google.com)
- Backend FastAPI corriendo (ver [trading-bot-api](https://github.com/alecorlo1314/trading-bot-api))

### Instalación

```powershell
# 1. Clonar el repositorio
git clone https://github.com/alecorlo1314/TradingBotApp.git
cd TradingBotApp

# 2. Abrir en Visual Studio
start TradingBotApp.sln
```

### Configuración de Firebase

1. Crear proyecto en [Firebase Console](https://console.firebase.google.com)
2. Agregar app Android con package name `com.tradingbot.app`
3. Descargar `google-services.json` y colocarlo en la raíz del proyecto
4. Verificar que la Build Action sea `GoogleServicesJson`

### Configurar la URL del backend

En `MauiProgram.cs`, actualiza la URL base del HttpClient:

```csharp
builder.Services.AddHttpClient<ITradingApiServicio, TradingApiServicio>(cliente =>
{
    // URL de tu backend en Railway (o localhost para desarrollo)
    cliente.BaseAddress = new Uri("https://tu-api.up.railway.app/");
    cliente.Timeout = TimeSpan.FromSeconds(30);
});
```

Para desarrollo local con emulador Android:
```csharp
cliente.BaseAddress = new Uri("http://10.0.2.2:8000/");
```

Para desarrollo local con celular físico (misma red WiFi):
```csharp
cliente.BaseAddress = new Uri("http://192.168.X.X:8000/");
```

### NuGet packages requeridos

```powershell
dotnet add package CommunityToolkit.Mvvm --version 8.3.2
dotnet add package MediatR --version 12.4.0
dotnet add package Plugin.Firebase.CloudMessaging --version 4.0.1
dotnet add package Plugin.Firebase.Core --version 4.0.1
dotnet add package Syncfusion.Maui.Charts
dotnet add package Syncfusion.Maui.Inputs
dotnet add package CommunityToolkit.Maui
```

---

## 📡 Endpoints de la API consumidos

| Método | Endpoint | Pestaña | Descripción |
|---|---|---|---|
| `GET` | `/api/account/balance` | Dashboard | Balance de cuenta |
| `GET` | `/api/account/positions` | Dashboard | Posiciones abiertas |
| `GET` | `/api/bot/status` | Dashboard / Bot | Estado del bot |
| `GET` | `/api/market/indicators/{symbol}` | Dashboard | RSI, MACD actuales |
| `GET` | `/api/market/history/{symbol}` | Dashboard | Precios para gráfica |
| `POST` | `/api/bot/start` | Bot Config | Iniciar bot |
| `POST` | `/api/bot/stop` | Bot Config | Detener bot |
| `POST` | `/api/bot/register-token` | App startup | Token FCM |
| `GET` | `/api/trades/history` | Historial | Últimas 20 órdenes |

---

## 🤖 ¿Cómo funciona el bot?

El bot corre en el servidor (Railway) cada X minutos de forma automática:

```
1. Verifica stop-loss → si pérdida > límite → vende
2. Descarga precios históricos desde Alpaca
3. Calcula RSI + MACD
4. Modelo LSTM predice precio del siguiente día
5. Combina señales → BUY / SELL / HOLD
6. Ejecuta orden en Alpaca (si hay señal)
7. Envía notificación push al celular
```

### Símbolos soportados

**Acciones (Lunes-Viernes 9:30am-4:00pm ET):**
`AAPL` `MSFT` `TSLA` `GOOGL` `AMZN` `NVDA` `META` `SPY` `QQQ` y miles más del NYSE/NASDAQ.

**Cripto (24/7):**
`BTC/USD` `ETH/USD` `SOL/USD` `DOGE/USD`

---

## 🏛 Patrones de diseño utilizados

- **Clean Architecture** — separación en capas Dominio / Aplicación / Infraestructura / UI
- **MVVM** — ViewModels con CommunityToolkit.Mvvm
- **CQRS con MediatR** — Consultas y Comandos separados
- **Result Pattern** — `Resultado<T>` para manejo de errores sin excepciones
- **Repository Pattern** — `ITradingApiServicio` como abstracción del acceso a datos
- **Dependency Injection** — todos los servicios y ViewModels registrados en DI

---

## 🔗 Repositorio del backend

El backend (FastAPI + Python) está en un repositorio separado:

**[alecorlo1314/trading-bot-api](https://github.com/alecorlo1314/trading-bot-api)**

Incluye: RSI+MACD · LSTM · APScheduler · Alpaca SDK · Firebase FCM v1 · Docker · Railway deploy

---

## 📄 Licencia

MIT License — libre para uso personal y educativo.

---

<div align="center">
  Construido con ❤️ por <a href="https://github.com/alecorlo1314">alecorlo1314</a>
</div>

- APIs de trading
