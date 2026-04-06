namespace Joaoaalves.Tiny.Core.Http;

/// <summary>
/// Configuration options for the Tiny API V2 client.
/// </summary>
public sealed class TinyOptions
{
    /// <summary>
    /// The API token used to authenticate every request.
    /// Obtain this from the Tiny panel under Configurações → Integrações → API.
    /// </summary>
    public required string Token { get; init; }

    /// <summary>
    /// Base URL of the Tiny API V2.
    /// Override only when targeting a test or staging environment.
    /// </summary>
    public Uri BaseAddress { get; init; } = new Uri("https://api.tiny.com.br/api2/");
}
