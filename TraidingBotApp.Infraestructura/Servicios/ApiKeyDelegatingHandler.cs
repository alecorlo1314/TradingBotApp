namespace TraidingBotApp.Infraestructura.Servicios;

/// <summary>
/// Intercepta cada petición HTTP e inyecta el header X-API-Key.
/// </summary>
public class ApiKeyDelegatingHandler : DelegatingHandler
{
    private readonly string _apiKey;

    public ApiKeyDelegatingHandler(string apiKey)
    {
        _apiKey = apiKey;
    }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        request.Headers.Add("X-API-Key", _apiKey);
        return base.SendAsync(request, cancellationToken);
    }
}
