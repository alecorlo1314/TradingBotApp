namespace TraidingBotApp.Dominio.Results;

public record Resultado
{
    public bool FueExitoso { get; }
    public bool EsError => !FueExitoso;

    public Error? Error { get; }

    protected Resultado(bool fueExitoso, Error? error)
    {
        if (fueExitoso && error != null)
            throw new ArgumentException("No puede haber error en éxito");

        if (!fueExitoso && error == null)
            throw new ArgumentException("Debe haber error en fallo");

        FueExitoso = fueExitoso;
        Error = error;
    }

    public static Resultado Exitoso() => new(true, null);
    public static Resultado Falla(Error error) => new(false, error);
}

public record Resultado<T> : Resultado
{
    private readonly T? _valor;

    public T Valor =>
        FueExitoso
            ? _valor!
            : throw new InvalidOperationException("No hay valor en un resultado fallido");

    private Resultado(T valor) : base(true, null)
    {
        _valor = valor ?? throw new ArgumentNullException(nameof(valor));
    }

    private Resultado(Error error) : base(false, error)
    {
    }

    public static implicit operator Resultado<T>(T value) => new(value);
    public static implicit operator Resultado<T>(Error error) => new(error);
}

public static class ResultadoExtensions
{
    public static Resultado<TOut> Map<TIn, TOut>(
        this Resultado<TIn> resultado,
        Func<TIn, TOut> map)
    {
        if (resultado.EsError)
            return resultado.Error!;

        return map(resultado.Valor);
    }

    public static TOut Match<TIn, TOut>(
    this Resultado<TIn> resultado,
    Func<TIn, TOut> onSuccess,
    Func<Error, TOut> onError)
    {
        return resultado.FueExitoso
            ? onSuccess(resultado.Valor)
            : onError(resultado.Error!);
    }
}


/// <summary>
/// Se define el tipo de error
/// </summary>
public enum TipoError
{
    // Errores de conectividad
    SinInternet,
    DnsNoResuelve,
    ConexionRechazada,
    RedInalcanzable,
    ConexionInterrumpida,
    ProxyError,
    SslHandshakeFallido,
    CertificadoExpirado,

    // Tiempo y Cancelacion
    TiempoExcedido,
    CanceladoPorElUsuario,
    CanceladoPorSistema,
    GatewayTimeout,
    ServicioNoDisponible,

    // HTTP / REST / API
    BadRequest,            // 400
    Unauthorized,          // 401
    Forbidden,             // 403
    NotFound,              // 404
    Conflict,              // 409
    TooManyRequests,       // 429 (throttling)
    InternalServerError,   // 500
    ServiceUnavailable,    // 503
    UnsupportedMediaType,  // 415
    ApiDeprecated,         // endpoint obsoleto

    // Autenticación y autorización
    CredencialesInvalidas,
    TokenExpirado,
    TokenNoValido,
    PermisoDenegado,
    CuentaBloqueada,

    // I/O y sistema
    ArchivoNoEncontrado,
    AccesoArchivoDenegado,
    DiscoLleno,
    ErrorEscritura,
    ErrorLectura,
    ErrorSerializacion,
    ErrorDeserializacion,
    ErrorConfiguracion,
    DependenciaFallida,    // fallo en servicio externo o dependencia
    CircuitoAbierto,       // circuit breaker

    // Validación y reglas de negocio
    ValidacionFallida,
    FormatoInvalido,
    DatosInconsistentes,
    ReglaNegocioViolada,
    RecursoBloqueado,
    RecursoEnUso,
    LimiteExcedido,        // cuota o límite de uso

    // Integración con servicios externos
    ServicioExternoNoDisponible,
    RespuestaInesperada,
    TiempoEsperaServicioExterno,
    ErrorProveedorPago,
    ErrorBaseDeDatos,
    TransaccionAbortada,
    IntegracionTimeout,

    // Concurrencia y sincronización
    ConflictoConcurrencia,
    VersionInvalida,
    BloqueoOptimistaFallido,

    // Genéricos / desconocidos
    ErrorDesconocido,
    NoImplementado,
    Cancelado,

    // Errores adicionales solicitados (mapeo directo desde la lista proporcionada)
    ErrorRed,
    ErrorTiempoEspera,
    ErrorJson,
    ErrorRespuestaVacia,
    ErrorHttpNoExitoso,
    ErrorAutenticacion,
    ErrorAutorizacion,
    ErrorSolicitudInvalida,
    ErrorRateLimit,
    ErrorServidor,
    ErrorMapeo,
    ErrorOperacionCancelada,
    ErrorInesperado
}


public record Error(string Codigo, TipoError TipoError, string Descripcion);

public static class Errores
{
    public static Error AccountNotFound { get; } = new("AccountNotFound", TipoError.ErrorRespuestaVacia, "La cuenta no fue encontrada o la API devolvió datos nulos");
    public static Error ErrorRed { get; } = new("NetworkError", TipoError.ErrorRed, "Error de red o HTTP al comunicarse con la API");
    public static Error Timeout { get; } = new("Timeout", TipoError.ErrorTiempoEspera, "La solicitud fue cancelada o expiró");
    public static Error JsonDeserialization { get; } = new("JsonDeserializationError", TipoError.ErrorJson, "Error al deserializar la respuesta JSON");
    public static Error HttpNonSuccess { get; } = new("HttpNonSuccess", TipoError.ErrorHttpNoExitoso, "La API respondió con un código HTTP no exitoso");
    public static Error Unauthorized { get; } = new("Unauthorized", TipoError.ErrorAutenticacion, "Autenticación fallida (401)");
    public static Error Forbidden { get; } = new("Forbidden", TipoError.ErrorAutorizacion, "No autorizado para acceder al recurso (403)");
    public static Error BadRequest { get; } = new("BadRequest", TipoError.ErrorSolicitudInvalida, "Solicitud inválida enviada a la API (400)");
    public static Error RateLimit { get; } = new("RateLimit", TipoError.ErrorRateLimit, "Se alcanzó el límite de peticiones (429)");
    public static Error ServerError { get; } = new("ServerError", TipoError.ErrorServidor, "Error interno del servidor (5xx)");
    public static Error MappingError { get; } = new("MappingError", TipoError.ErrorMapeo, "Error al mapear DTO a entidad de dominio");
    public static Error ConfigurationError { get; } = new("ConfigurationError", TipoError.ErrorConfiguracion, "Error en la configuración del cliente o serializador");
    public static Error OperationCanceled { get; } = new("OperationCanceled", TipoError.ErrorOperacionCancelada, "La operación fue cancelada por el token");
    public static Error Unexpected { get; } = new("UnexpectedError", TipoError.ErrorInesperado, "Error inesperado en la operación");
    public static Error SinInternet { get; } = new("SinInternet", TipoError.SinInternet, "No hay conectividad a Internet");
    public static Error DnsNoResuelve { get; } = new("DnsNoResuelve", TipoError.DnsNoResuelve, "El DNS no resuelve el host");
    public static Error ConexionRechazada { get; } = new("ConexionRechazada", TipoError.ConexionRechazada, "La conexión fue rechazada por el servidor");
    public static Error RedInalcanzable { get; } = new("RedInalcanzable", TipoError.RedInalcanzable, "La red es inalcanzable desde el cliente");
    public static Error ConexionInterrumpida { get; } = new("ConexionInterrumpida", TipoError.ConexionInterrumpida, "La conexión se interrumpió durante la transferencia");
    public static Error ProxyError { get; } = new("ProxyError", TipoError.ProxyError, "Error al comunicarse a través del proxy");
    public static Error SslHandshakeFallido { get; } = new("SslHandshakeFallido", TipoError.SslHandshakeFallido, "Fallo en el handshake SSL/TLS");
    public static Error CertificadoExpirado { get; } = new("CertificadoExpirado", TipoError.CertificadoExpirado, "El certificado SSL/TLS ha expirado");

    public static Error TiempoExcedido { get; } = new("TiempoExcedido", TipoError.TiempoExcedido, "La solicitud excedió el tiempo de espera");
    public static Error CanceladoPorElUsuario { get; } = new("CanceladoPorElUsuario", TipoError.CanceladoPorElUsuario, "La operación fue cancelada por el usuario");
    public static Error CanceladoPorSistema { get; } = new("CanceladoPorSistema", TipoError.CanceladoPorSistema, "La operación fue cancelada por el sistema");
    public static Error GatewayTimeout { get; } = new("GatewayTimeout", TipoError.GatewayTimeout, "Tiempo de espera en gateway o proxy");
    public static Error ServicioNoDisponible { get; } = new("ServicioNoDisponible", TipoError.ServicioNoDisponible, "El servicio no está disponible temporalmente");

    public static Error MalaSolicitud { get; } = new("BadRequest", TipoError.BadRequest, "Solicitud inválida (400)");
    public static Error NoAutenticado { get; } = new("Unauthorized", TipoError.Unauthorized, "No autenticado (401)");
    public static Error AccesoDenegado { get; } = new("AccesoDenegado", TipoError.Forbidden, "Acceso denegado (403)");
    public static Error NotFound { get; } = new("NotFound", TipoError.NotFound, "Recurso no encontrado (404)");
    public static Error Conflict { get; } = new("Conflict", TipoError.Conflict, "Conflicto en la solicitud (409)");
    public static Error TooManyRequests { get; } = new("TooManyRequests", TipoError.TooManyRequests, "Límite de peticiones alcanzado (429)");
    public static Error InternalServerError { get; } = new("InternalServerError", TipoError.InternalServerError, "Error interno del servidor (500)");
    public static Error ServiceUnavailable { get; } = new("ServiceUnavailable", TipoError.ServiceUnavailable, "Servicio temporalmente no disponible (503)");
    public static Error UnsupportedMediaType { get; } = new("UnsupportedMediaType", TipoError.UnsupportedMediaType, "Tipo de contenido no soportado (415)");
    public static Error ApiDeprecated { get; } = new("ApiDeprecated", TipoError.ApiDeprecated, "El endpoint está obsoleto");

    public static Error CredencialesInvalidas { get; } = new("CredencialesInvalidas", TipoError.CredencialesInvalidas, "Credenciales inválidas");
    public static Error TokenExpirado { get; } = new("TokenExpirado", TipoError.TokenExpirado, "El token de autenticación expiró");
    public static Error TokenNoValido { get; } = new("TokenNoValido", TipoError.TokenNoValido, "El token proporcionado no es válido");
    public static Error PermisoDenegado { get; } = new("PermisoDenegado", TipoError.PermisoDenegado, "Permiso denegado para la operación");
    public static Error CuentaBloqueada { get; } = new("CuentaBloqueada", TipoError.CuentaBloqueada, "La cuenta se encuentra bloqueada");

    public static Error ArchivoNoEncontrado { get; } = new("ArchivoNoEncontrado", TipoError.ArchivoNoEncontrado, "Archivo no encontrado");
    public static Error AccesoArchivoDenegado { get; } = new("AccesoArchivoDenegado", TipoError.AccesoArchivoDenegado, "Acceso al archivo denegado");
    public static Error DiscoLleno { get; } = new("DiscoLleno", TipoError.DiscoLleno, "Espacio en disco insuficiente");
    public static Error ErrorEscritura { get; } = new("ErrorEscritura", TipoError.ErrorEscritura, "Error al escribir en el sistema de archivos");
    public static Error ErrorLectura { get; } = new("ErrorLectura", TipoError.ErrorLectura, "Error al leer desde el sistema de archivos");
    public static Error ErrorSerializacion { get; } = new("ErrorSerializacion", TipoError.ErrorSerializacion, "Error al serializar datos");
    public static Error ErrorDeserializacion { get; } = new("ErrorDeserializacion", TipoError.ErrorDeserializacion, "Error al deserializar datos");
    public static Error ErrorConfiguracion { get; } = new("ErrorConfiguracion", TipoError.ErrorConfiguracion, "Error en la configuración del sistema");
    public static Error DependenciaFallida { get; } = new("DependenciaFallida", TipoError.DependenciaFallida, "Fallo en una dependencia externa");
    public static Error CircuitoAbierto { get; } = new("CircuitoAbierto", TipoError.CircuitoAbierto, "Circuit breaker abierto por fallos repetidos");

    public static Error ValidacionFallida { get; } = new("ValidacionFallida", TipoError.ValidacionFallida, "Validación de entrada fallida");
    public static Error FormatoInvalido { get; } = new("FormatoInvalido", TipoError.FormatoInvalido, "Formato de datos inválido");
    public static Error DatosInconsistentes { get; } = new("DatosInconsistentes", TipoError.DatosInconsistentes, "Datos inconsistentes detectados");
    public static Error ReglaNegocioViolada { get; } = new("ReglaNegocioViolada", TipoError.ReglaNegocioViolada, "Se violó una regla de negocio");
    public static Error RecursoBloqueado { get; } = new("RecursoBloqueado", TipoError.RecursoBloqueado, "El recurso está bloqueado");
    public static Error RecursoEnUso { get; } = new("RecursoEnUso", TipoError.RecursoEnUso, "El recurso se encuentra en uso");
    public static Error LimiteExcedido { get; } = new("LimiteExcedido", TipoError.LimiteExcedido, "Se excedió el límite permitido");

    public static Error ServicioExternoNoDisponible { get; } = new("ServicioExternoNoDisponible", TipoError.ServicioExternoNoDisponible, "Servicio externo no disponible");
    public static Error RespuestaInesperada { get; } = new("RespuestaInesperada", TipoError.RespuestaInesperada, "La respuesta del servicio externo fue inesperada");
    public static Error TiempoEsperaServicioExterno { get; } = new("TiempoEsperaServicioExterno", TipoError.TiempoEsperaServicioExterno, "Tiempo de espera al llamar servicio externo");
    public static Error ErrorProveedorPago { get; } = new("ErrorProveedorPago", TipoError.ErrorProveedorPago, "Error en proveedor de pagos");
    public static Error ErrorBaseDeDatos { get; } = new("ErrorBaseDeDatos", TipoError.ErrorBaseDeDatos, "Error en la base de datos");
    public static Error TransaccionAbortada { get; } = new("TransaccionAbortada", TipoError.TransaccionAbortada, "Transacción abortada");
    public static Error IntegracionTimeout { get; } = new("IntegracionTimeout", TipoError.IntegracionTimeout, "Timeout en integración con servicio externo");

    public static Error ConflictoConcurrencia { get; } = new("ConflictoConcurrencia", TipoError.ConflictoConcurrencia, "Conflicto por concurrencia");
    public static Error VersionInvalida { get; } = new("VersionInvalida", TipoError.VersionInvalida, "Versión inválida para la operación");
    public static Error BloqueoOptimistaFallido { get; } = new("BloqueoOptimistaFallido", TipoError.BloqueoOptimistaFallido, "Fallo en bloqueo optimista");

    public static Error ErrorDesconocido { get; } = new("ErrorDesconocido", TipoError.ErrorDesconocido, "Error desconocido");
    public static Error NoImplementado { get; } = new("NoImplementado", TipoError.NoImplementado, "Funcionalidad no implementada");
    public static Error Cancelado { get; } = new("Cancelado", TipoError.Cancelado, "Operación cancelada");
}