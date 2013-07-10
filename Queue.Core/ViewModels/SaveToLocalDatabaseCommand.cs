using System.Collections.Generic;

namespace Queue.Core.ViewModels
{
	public class SaveToLocalDatabaseCommand : IQueuedCommand
	{

		public SaveToLocalDatabaseCommand()
		{
			_onSuccessCommands = new List<IQueuedCommand>();
			_onErrorCommands = new List<IQueuedCommand>();
		}
		public string Message { get; set; }
		public void Execute()
		{
			var x = Message;
		}

		private readonly IList<IQueuedCommand> _onSuccessCommands;
		public IEnumerable<IQueuedCommand> OnSuccessCommands
		{
			get { return _onSuccessCommands; }
		}

		public void AddSuccessCommand(IQueuedCommand command)
		{
			_onSuccessCommands.Add(command);
		}

		public void RemoveSuccessCommand(IQueuedCommand command)
		{
			throw new System.NotImplementedException();
		}

		private readonly IList<IQueuedCommand> _onErrorCommands;
		
		public IEnumerable<IQueuedCommand> OnErrorCommands
		{
			get
			{
				return _onErrorCommands;
			}
		}

		public void AddErrorCommand(IQueuedCommand command)
		{
			_onErrorCommands.Add(command);
		}

		public void RemoveErrorCommand(IQueuedCommand command)
		{
			throw new System.NotImplementedException();
		}
	}
}