using Newtonsoft.Json;
// ReSharper disable UnusedMember.Global
// ReSharper disable once CheckNamespace

namespace Microsoft.AspNetCore.Http;

public static class SessionExtensionsTuke
{
    public static T? GetObject<T>(this ISession session, string key)
    {
        ArgumentNullException.ThrowIfNull(session);
        ArgumentNullException.ThrowIfNull(key);

        var str = session.GetString(key);
        return str == null ? default : JsonConvert.DeserializeObject<T>(str);
    }

    public static void SetObject<T>(this ISession session, string key, T value)
    {
        ArgumentNullException.ThrowIfNull(session);
        ArgumentNullException.ThrowIfNull(key);
        ArgumentNullException.ThrowIfNull(value);

        session.SetString(key, JsonConvert.SerializeObject(value));
    }

    public static bool ExistsKey(this ISession session, string key)
    {
        ArgumentNullException.ThrowIfNull(session);
        ArgumentNullException.ThrowIfNull(key);
        return session.TryGetValue(key, out _);
    }
}
