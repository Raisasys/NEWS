using Core;

namespace Service.EventBus
{
    public static class EventBusSetting
	{
        public static TimeSpan MinimumDelayTimeInSeconds => Config.Get<int>("EventHost:MinimumDelayTimeInSeconds").Seconds();
        public static string HangfireConnectionString => Config.Get("ConnectionStrings:HangfireDatabase");
        internal static string RedisConnectionString => Config.Get("ConnectionStrings:Redis") ?? "localhost:6379";
        
        public const string HandleMethodName = "Handle";

	}
}

