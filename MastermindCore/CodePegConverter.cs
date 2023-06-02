using Newtonsoft.Json;

namespace MastermindCore;

internal class CodePegConverter : JsonConverter<CodePeg>
{
    /// <inheritdoc />
    public override void WriteJson(JsonWriter writer, CodePeg? value, JsonSerializer serializer)
    {
        ArgumentNullException.ThrowIfNull(value);
        serializer.Serialize(writer, value.Id);
    }

    /// <inheritdoc />
    public override CodePeg ReadJson(JsonReader reader, Type objectType, CodePeg? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var value = serializer.Deserialize<int>(reader);
        return CodePeg.Parse(value);
    }
}