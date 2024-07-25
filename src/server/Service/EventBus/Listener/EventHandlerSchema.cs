using System.Reflection;
using Newtonsoft.Json.Linq;

namespace Service.EventBus;

public class EventHandlerSchema
{
	public EventHandlerSchema(MethodInfo handleMethod, Type handlerType)
	{
		HandleMethod = handleMethod;
		HandlerType = handlerType;
		EventType = handleMethod.GetParameters().First().ParameterType;
		Key = EventType.Name;
		
	}

	public string Key { get; }
	public Type EventType { get; }
	public MethodInfo HandleMethod { get; }
	public Type HandlerType { get; }

	public object CreateEventInstance(JObject jObject) => jObject.ToObject(EventType);
}