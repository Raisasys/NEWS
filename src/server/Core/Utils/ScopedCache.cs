using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;

namespace Core;

public interface IScopedCache
{
    void Add<TValue>(string key, TValue value);
    void Remove(string key);
    TValue Get<TValue>(string key);
    object Get(string key);
    bool Has(string key);
}
public class ScopedCache : IScopedCache
{
    private readonly ConcurrentDictionary<string, object> _cache = new ConcurrentDictionary<string, object>();

    public void Add<TValue>(string key, TValue value) => _cache.AddOrUpdate(key, value, (s, o) => value);

    public void Remove(string key) => _cache.TryRemove(key, out var data);

    public TValue Get<TValue>(string key)
    {
        if (_cache.TryGetValue(key, out var data))
        {
            if (data is TValue value)
                return value;
        }
        return default;
    }

    public object Get(string key) => _cache[key];

    public bool Has(string key) => _cache.ContainsKey(key);
}