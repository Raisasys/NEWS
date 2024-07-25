using Core.EventBus;
using StackExchange.Redis;
using Newtonsoft.Json;
using Hangfire;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;

namespace Service.EventBus;

public class EventListener
{
	private readonly ConcurrentDictionary<string, EventHandlerSchema> _schemaItems = new();
	private readonly IServiceProvider _serviceProvider;
	private readonly IDistributedCache _delayedEventStorage;
	public string Channel { get; }
	
	public async Task OnMessageHandler(ChannelMessage channelMessage)
	{
		try
		{
			string channel = channelMessage.Channel;
			string message = channelMessage.Message;
			var eventBusMessage = JsonConvert.DeserializeObject<EventBusMessageDeserializer>(message);

			if (eventBusMessage.ShouldBeExecute)
				await CallEventConsumer(eventBusMessage);
			else
				await CallDelayedEventConsumer(channel, eventBusMessage);

		}
		catch (Exception exception)
		{
			Console.WriteLine(exception);
		}
	}

	public void RegisterEventHandler(Type eventHandlerType)
		=> ResolveHandleMethods(eventHandlerType);

	public void RegisterEventHandler<TEventBusChannel, TEventHandler>()
		where TEventHandler : IEventHandler<TEventBusChannel>
		where TEventBusChannel : IEventBusChannel
		=> RegisterEventHandler(typeof(TEventHandler));


	private void ResolveHandleMethods(Type eventHandlerType)
	{
		var handleMethods = eventHandlerType.GetMethods().Where(i => i.Name == EventBusSetting.HandleMethodName).ToList();
		foreach (var handleMethod in handleMethods)
		{
			var eventListenerSchema = new EventHandlerSchema(handleMethod, eventHandlerType);
			_schemaItems.TryAdd(eventListenerSchema.Key, eventListenerSchema);
		}
	}

	private async Task CallEventConsumer(EventBusMessageDeserializer eventBusMessage)
	{
		var key = eventBusMessage.Key;
		_schemaItems.TryGetValue(key, out var listenerSchema);

		if (listenerSchema == null) return;
		
		var eventInstance = listenerSchema.CreateEventInstance(eventBusMessage.Event);

		var serviceScopeFactory = _serviceProvider.GetRequiredService<IServiceScopeFactory>();
		var activator = new EventHandlerActivator(serviceScopeFactory.CreateScope());
		var handler = activator.Resolve(listenerSchema.HandlerType);
		try
		{
			await listenerSchema.HandleMethod.InvokeAsync(handler, eventInstance);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
		}
		finally
		{
			activator.DisposeScope();
		}
	}

	private async Task CallDelayedEventConsumer(string channel, EventBusMessageDeserializer eventBusMessage)
	{
		var addNewJob = true;
		var delayTime = EventBusSetting.MinimumDelayTimeInSeconds > eventBusMessage.Delayed ? EventBusSetting.MinimumDelayTimeInSeconds : eventBusMessage.Delayed;
		var delayedEventKey = eventBusMessage.EventKey;
		var existJobId = await _delayedEventStorage.GetStringAsync(delayedEventKey);
		if (!string.IsNullOrEmpty(existJobId))
		{
			var existJob = JobStorage.Current.GetConnection().GetJobData(existJobId);
			if (existJob != null)
			{
				var existJobState = existJob.State.ToLower();
				if (StatesToDelayJobAgain.Contains(existJobState))
				{
					BackgroundJob.Reschedule(existJobId, delayTime);
					addNewJob = false;
				}
				else if (StatesToDoubleDelayJobAgain.Contains(existJobState))
				{
					BackgroundJob.Reschedule(existJobId, delayTime.Add(delayTime).Add(delayTime));
					addNewJob = false;
				}
				else
				{
					BackgroundJob.Delete(existJobId);
				}
			}
		}

		if (addNewJob)
		{
			eventBusMessage.ShouldBeExecute = true;

			/*Expression<Func<Task>> methodCall = () => EventBus.Bus(channel).SendEventBusMessageAsync(eventBusMessage);
			var jobId = BackgroundJob.Schedule(methodCall, delayTime);*/

			var channelName = channel.ToString();
			var serializedMessage = eventBusMessage.Serialized;
			var jobId = BackgroundJob.Schedule<IDelayedEventListenerInvokerJob>(x => x.Call(channelName, serializedMessage), delayTime);

			await _delayedEventStorage.SetStringAsync(delayedEventKey, jobId);
		}
		//
	}
	
	private static readonly List<string> StatesToDelayJobAgain = new() { "enqueued", "scheduled" };
	private static readonly List<string> StatesToDoubleDelayJobAgain = new() { "processing", "awaiting" };

	public EventListener(string channel, IServiceProvider serviceProvider)
	{
		Channel = channel;
		_serviceProvider = serviceProvider;
		_delayedEventStorage = serviceProvider.GetRequiredService<IDistributedCache>();
	}
}