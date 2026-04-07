using System.Text.Json;
using System.Text.Json.Serialization;

namespace Joaoaalves.Tiny.Core.Http;

/// <summary>
/// Deserializes any scalar JSON token (string, number, boolean) into a
/// <see cref="string"/> or <see langword="null"/>.
/// The Tiny API V2 returns numeric fields inconsistently — sometimes as JSON
/// strings (<c>"1"</c>), sometimes as JSON numbers (<c>1</c>). Registering
/// this converter on the response options handles both without changes to the DTOs.
/// </summary>
internal sealed class FlexibleStringConverter : JsonConverter<string?>
{
    public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.TokenType switch
        {
            JsonTokenType.String => reader.GetString(),
            JsonTokenType.Number => System.Text.Encoding.UTF8.GetString(reader.ValueSpan),
            JsonTokenType.True   => "true",
            JsonTokenType.False  => "false",
            JsonTokenType.Null   => null,
            _                    => System.Text.Encoding.UTF8.GetString(reader.ValueSpan)
        };

    public override void Write(Utf8JsonWriter writer, string? value, JsonSerializerOptions options)
    {
        if (value is null) writer.WriteNullValue();
        else writer.WriteStringValue(value);
    }
}
