using System.Collections.Generic;

namespace Queue.Core.ViewModels
{
	public interface IQueuedCommand
	{
		void Execute();
		IDictionary<string, object> OnSuccessCommands { get; }
		void AddSuccessCommand(IQueuedCommand command);
		void RemoveSuccessCommand(IQueuedCommand command);

		IDictionary<string, object> OnErrorCommands { get; }
		void AddErrorCommand(IQueuedCommand command);
		void RemoveErrorCommand(IQueuedCommand command);
	}
}