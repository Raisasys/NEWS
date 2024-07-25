namespace Service.EventBus
{
    public interface IDelayedEventListenerInvokerJob
	{
        Task Call(string channel, string serializedMessage);
    }

    public class DelayedEventListenerInvokerJob : IDelayedEventListenerInvokerJob
	{
       
        public async Task Call(string channel, string serializedMessage)
        {
            await EventBus.SendStringMessageAsync(channel, serializedMessage);
        }
    }
}
