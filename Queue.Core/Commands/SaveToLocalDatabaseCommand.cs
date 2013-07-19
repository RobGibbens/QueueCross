using System.Collections.Generic;

namespace Queue.Core.ViewModels
{
	using System;
	using System.IO;

	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	public class SaveToLocalDatabaseCommand : IQueuedCommand
	{

		public SaveToLocalDatabaseCommand()
		{
			_onSuccessCommands = new Dictionary<string, object>();
			_onErrorCommands = new Dictionary<string, object>();
		}
		public string Message { get; set; }
		public void Execute()
		{
			var x = Message;
			foreach (var onSuccessCommand in OnSuccessCommands)
			{
				var typeName = onSuccessCommand.Key;
				var jObject = onSuccessCommand.Value as JObject;

				var serializer = new JsonSerializer();
				var type = Type.GetType(typeName);
				var command = (IQueuedCommand)serializer.Deserialize(new JTokenReader(jObject), type);
				command.Execute();
			}
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