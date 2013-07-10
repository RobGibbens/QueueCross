using System.Collections.Generic;

namespace Queue.Core.ViewModels
{
	public interface IQueuedCommand
	{
		void Execute();
		IEnumerable<IQueuedCommand> OnSuccessCommands { get; }
		void AddSuccessCommand(IQueuedCommand command);
		void RemoveSuccessCommand(IQueuedCommand command);
		
		IEnumerable<IQueuedCommand> OnErrorCommands { get; }
		void AddErrorCommand(IQueuedCommand command);
		void RemoveErrorCommand(IQueuedCommand command);
	}
}