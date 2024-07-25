using Core.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Service.EventBus
{
    public class EventBusMessageSerializer<TEvent> where TEvent : IEventBase
    {
        private TEvent _event;

        public EventBusMessageSerializer(TEvent @event)
        {
            _event = @event;
        }

        public string EventTypeName => _event.GetType().Name;
        public TEvent Event => _event;
        public bool ShouldBeExecute => _event.ShouldBeExecute;
        public string EventKey => _event is IDelayedEvent @event
            ? $"{_event.GetType().Name}_{@event.EventKey}"
            : _event.EventId.ToString();
        public TimeSpan Delayed => _event is IDelayedEvent @event
            ? @event.Delayed
            : TimeSpan.Zero;


        [JsonIgnore]
        public string Serialized => JsonConvert.SerializeObject(this);
    }


    public class EventBusMessageDeserializer
    {
        public string EventTypeName { get; set; }
        public JObject Event { get; set; }
        public bool ShouldBeExecute { get; set; }
        public string EventKey { get; set; }
        public TimeSpan Delayed { get; set; }

        public string Key => EventTypeName;

        [JsonIgnore]
        public string Serialized => JsonConvert.SerializeObject(this);
    }
}
