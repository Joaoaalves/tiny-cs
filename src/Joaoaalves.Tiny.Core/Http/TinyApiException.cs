using Joaoaalves.Tiny.Core.DTOs.Common;

namespace Joaoaalves.Tiny.Core.Http;

/// <summary>
/// Thrown when the Tiny API returns a business-level error (status "Erro")
/// or when the response cannot be interpreted.
/// </summary>
public sealed class TinyApiException : Exception
{
    /// <summary>The status string returned by Tiny (typically "Erro").</summary>
    public string? ApiStatus { get; }

    /// <summary>Error messages extracted from the Tiny response payload.</summary>
    public IReadOnlyList<string> ApiErrors { get; }

    /// <summary>Initialises the exception with a plain message (e.g. empty response).</summary>
    public TinyApiException(string message) : base(message)
    {
        ApiErrors = [];
    }

    internal TinyApiException(TinyApiBaseResponse response)
        : base(BuildMessage(response))
    {
        ApiStatus = response.Status;
        ApiErrors = response.Errors?
            .Select(e => e.Error ?? string.Empty)
            .Where(e => e.Length > 0)
            .ToList() ?? [];
    }

    private static string BuildMessage(TinyApiBaseResponse response)
    {
        var errors = response.Errors?
            .Select(e => e.Error)
            .Where(e => !string.IsNullOrEmpty(e))
            .ToList();

        if (errors is { Count: > 0 })
            return $"Tiny API error: {string.Join("; ", errors)}";

        return $"Tiny API returned non-OK status: {response.Status}";
    }
}
