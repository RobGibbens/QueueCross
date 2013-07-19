namespace Queue.Core.ViewModels
{
	public interface IMvxQueue
	{
		void Push(IQueuedCommand command);
		void ProcessQueue();
	}
}