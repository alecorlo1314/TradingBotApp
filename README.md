<p align="center">
	<img src="./imagen_app_trading.svg" alt="TradingBotApp Banner" width="800">
</p>

<p align="center">
	<em>Aplicación multiplataforma para automatización de trading</em>
</p>

<p align="center">
	<img src="https://img.shields.io/github/license/alecorlo1314/TradingBotApp?style=default&logo=opensourceinitiative&logoColor=white&color=0080ff" alt="license">
	<img src="https://img.shields.io/github/last-commit/alecorlo1314/TradingBotApp?style=default&logo=git&logoColor=white&color=0080ff" alt="last-commit">
	<img src="https://img.shields.io/github/languages/top/alecorlo1314/TradingBotApp?style=default&color=0080ff" alt="repo-top-language">
	<img src="https://img.shields.io/github/languages/count/alecorlo1314/TradingBotApp?style=default&color=0080ff" alt="repo-language-count">
</p>

---

## 📚 Tabla de Contenidos

- [📖 Descripción](#-descripción)
- [🚀 Características](#-características)
- [🏗️ Estructura del Proyecto](#-estructura-del-proyecto)
- [⚙️ Instalación](#-instalación)
- [▶️ Uso](#️-uso)
- [🧪 Testing](#-testing)
- [🛣️ Roadmap](#-roadmap)
- [🤝 Contribución](#-contribución)
- [📄 Licencia](#-licencia)

---

## 📖 Descripción

**TradingBotApp** es una aplicación desarrollada en .NET MAUI que permite configurar, ejecutar y monitorear un bot de trading automatizado.

La arquitectura del proyecto sigue principios de **Clean Architecture**, separando responsabilidades en capas:

- UI (Frontend) → Interfaz de usuario multiplataforma
- Aplicación → Casos de uso (CQRS: comandos y consultas)
- Dominio → Entidades y lógica de negocio
- Infraestructura → Integración con APIs externas

---

## 🚀 Características

- 🤖 Configuración y control de bots de trading
- 📊 Visualización de operaciones y métricas
- 📈 Consulta de indicadores técnicos
- 💰 Monitoreo de balance y posiciones
- 🔔 Sistema de notificaciones
- 🔌 Integración con APIs externas
- 🧱 Arquitectura limpia (Clean Architecture + CQRS)

---

## 🏗️ Estructura del Proyecto

El proyecto está organizado en múltiples capas:

- TradingBotApp (UI): Interfaz de usuario en .NET MAUI
- Aplicación: Lógica de negocio (comandos, consultas, DTOs)
- Dominio: Entidades, reglas de negocio e interfaces
- Infraestructura: Servicios externos y mappers

---

## ⚙️ Instalación

```sh
git clone https://github.com/alecorlo1314/TradingBotApp
cd TradingBotApp
dotnet restore
```

---

## ▶️ Uso

```sh
dotnet run
```

---

## 🧪 Testing

```sh
dotnet test
```

---

## 🛣️ Roadmap

- [x] Implementación base del bot
- [ ] Mejora de estrategias de trading
- [ ] Integración con más brokers
- [ ] Dashboard avanzado con gráficos
- [ ] Sistema de alertas en tiempo real

---

## 🤝 Contribución

1. Haz un fork del proyecto  
2. Crea una rama (`feature/nueva-funcionalidad`)  
3. Haz commit de tus cambios  
4. Abre un Pull Request  

---

## 📄 Licencia

Este proyecto está bajo licencia MIT.

---

## 🙌 Agradecimientos

- Comunidad .NET
- .NET MAUI
- APIs de trading
