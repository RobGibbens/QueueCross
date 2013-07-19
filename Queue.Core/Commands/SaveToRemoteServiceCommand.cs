using System.Collections.Generic;

namespace Queue.Core.ViewModels
{
	public class SaveToRemoteServiceCommand : IQueuedCommand
	{
		public SaveToRemoteServiceCommand()
		{
			_onErrorCommands = new Dictionary<string, object>();
			_onSuccessCommands = new Dictionary<string, object>();
		}
		public int TimesToKick { get; set; }
		public int TimesToLaugh { get; set; }
		public string InsultToSay { get; set; }
		public void Execute()
		{
			var s = "";
		}

		private readonly IDictionary<string, object> _onSuccessCommands;
		public IDictionary<string, object> OnSuccessCommands
		{
			get { return _onSuccessCommands; }
		}

		public void AddSuccessCommand(IQueuedCommand command)
		{
			var typeName = command.GetType().ToString();
			_onSuccessCommands.Add(new KeyValuePair<string, object>(typeName, command));
		}

		public void RemoveSuccessCommand(IQueuedCommand command)
		{
			throw new System.NotImplementedException();
		}

		private readonly IDictionary<string, object> _onErrorCommands;

		public IDictionary<string, object> OnErrorCommands
		{
			get
			{
				return _onErrorCommands;
			}
		}

		public void AddErrorCommand(IQueuedCommand command)
		{
			var typeName = command.GetType().ToString();
			_onErrorCommands.Add(new KeyValuePair<string, object>(typeName, command));
		}

		public void RemoveErrorCommand(IQueuedCommand command)
		{
			throw new System.NotImplementedException();
		}
	}
}