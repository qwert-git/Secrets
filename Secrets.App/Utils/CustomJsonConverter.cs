using Newtonsoft.Json;
using Secrets.App.Exceptions;

namespace Secrets.App.Utils;

internal static class CustomJsonConverter
{
    private static JsonSerializerSettings settigns = new JsonSerializerSettings
    {
        MissingMemberHandling = MissingMemberHandling.Error
    };
    public static string Serialize<T>(T obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    public static T Deserialize<T>(string json)
    {
        try
        {
            return JsonConvert.DeserializeObject<T>(json, settigns);
        }
        catch (JsonException ex)
        {
            throw new SecretsAppJsonConverterException($"Wrong json formant. {ex.Message}");
        }
    }
}