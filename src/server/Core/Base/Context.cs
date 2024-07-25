using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using static Core.Context;

namespace Core;

public class Context
{
    static Context _current = null;
    //internal static Func<Context> ContextProvider = () => new Context();

    IServiceProvider _applicationServices;
    Func<IServiceProvider> _scopeServiceHttpProvider;
    Func<IServiceProvider> _scopeServiceWorkerProvider;
    Func<IServiceProvider> _scopeServiceEventBusListenerProvider;

	public IServiceProvider ServiceProvider =>
		_scopeServiceWorkerProvider?.Invoke() ??
		_scopeServiceEventBusListenerProvider?.Invoke() ??
		_scopeServiceHttpProvider?.Invoke() ?? 
		_applicationServices;

    /// <summary>
    /// Occurs when the StartUp.OnInitializedAsync is completed.
    /// </summary>
    public static event AwaitableEventHandler StartedUp;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static Task OnStartedUp() => StartedUp.Raise();

    public static Context Current => _current ?? throw new InvalidOperationException("Context is not initialized!");

    
    public static IUowDatabase GetDatabase() => _current.GetService<IUowDatabase>();
    public static ILogger GetLogger(string loggerName = "App") => _current.GetService<ILoggerFactory>().CreateLogger(loggerName);
    public static ILogger GetLogger<T>() => _current.GetService<ILoggerFactory>().CreateLogger<T>();
    


    public static Context Initialize(
	    IServiceProvider applicationServices,
	    Func<IServiceProvider> scopeServiceHttpProvider,
		Func<IServiceProvider> scopeServiceWorkerProvider,
	    Func<IServiceProvider> scopeServiceEventBusListenerProvider)
    {
        return _current = new()
        {
            _applicationServices = applicationServices,
            _scopeServiceHttpProvider= scopeServiceHttpProvider,
            _scopeServiceWorkerProvider = scopeServiceWorkerProvider,
            _scopeServiceEventBusListenerProvider= scopeServiceEventBusListenerProvider
		};
    }

    public TService GetService<TService>() => ServiceProvider.GetRequiredService<TService>();

    public IConfiguration Config => GetService<IConfiguration>();

    public TService GetOptionalService<TService>() where TService : class
    {
        var result = ServiceProvider.GetService<TService>();

        if (result == null)
            Debug.WriteLine(typeof(TService).FullName + " service is not configured.");

        return result;
    }

    public IEnumerable<TService> GetServices<TService>() => ServiceProvider.GetServices<TService>();

    public static Context StartupInitialize(IServiceProvider scopeServiceProvider)
    {
        return _current = new()
        {
            _applicationServices = scopeServiceProvider,
            //ScopeServiceProvider = scopeServiceProvider
        };
        //appApplicationServices = scopeServiceProvider;
    }

    public static void DisposeStartupInitialize()
    {
        _current = new();
        //appApplicationServices = null;
    }
    

    public static DateTimeOffset NowAsDateTimeOffset() => DateTimeOffset.UtcNow;

    private static class CacheByRequest
    {
        private static IScopedCache Cache => Current.GetService<IScopedCache>();


        public static object Get(string key)
            => Cache.Get(key);


        public static T Get<T>(string key)
            => Cache.Get<T>(key);

        public static bool Has(string key)
            => Cache.Has(key);


        public static void Set(string key, object value)
            => Cache.Add(key, value);


        public static void Remove(string key)
            => Cache.Remove(key);

        public static T GetOrAdd<T>(string key, Func<T> func)
        {
            if (Has(key)) return Get<T>(key);
            var result = func();
            Set(key, result);
            return result;
        }

    }

    public static class OnCurrent
    {
        static string CURRENT_CACHE_KEY(string name) => $"Current.Primitive.{name}";

        public static bool Has(string name)
            => CacheByRequest.Has(CURRENT_CACHE_KEY(name));

        public static object Get(string name)
            => CacheByRequest.Get(CURRENT_CACHE_KEY(name));

        public static void Set(string name,object obj)
            => CacheByRequest.Set(CURRENT_CACHE_KEY(name), obj);
    }

    public static class OnCurrent<T>
    {
        static string CURRENT_CACHE_KEY => $"Current.{typeof(T).Name}";

        public static bool Has()
            => CacheByRequest.Has(CURRENT_CACHE_KEY);

        public static T Get()
            => CacheByRequest.Get<T>(CURRENT_CACHE_KEY);
        
        public static void Set(T user)
            => CacheByRequest.Set(CURRENT_CACHE_KEY, user);
    }
}