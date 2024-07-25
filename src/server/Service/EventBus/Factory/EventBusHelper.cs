using System.Reflection;
using Core.EventBus;
using Core.Events;

namespace Service.EventBus
{
    public static class EventBusHelper
    {
	    public static async Task InvokeAsync(this MethodInfo @this, object obj, params object[] parameters)
        {
            dynamic awaitable = @this.Invoke(obj, parameters);
            await awaitable;
            //awaitable.GetAwaiter();
        }


        public static void Push<TEventBusChannel, TEvent>(this TEvent @this)
	        where TEvent : IEvent<TEventBusChannel>
			where TEventBusChannel: IEventBusChannel
            => EventBus.SendEvent<TEventBusChannel, TEvent>(@this);

        public static Task PushAsync<TEventBusChannel, TEvent>(this TEvent @this)
	        where TEvent : IEvent<TEventBusChannel>
	        where TEventBusChannel : IEventBusChannel
	        => EventBus.SendEventAsync<TEventBusChannel, TEvent>(@this);



	}
}


