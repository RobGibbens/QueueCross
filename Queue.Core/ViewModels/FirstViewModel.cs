using Cirrious.MvvmCross.ViewModels;

namespace Queue.Core.ViewModels
{
	using System;
	using System.Linq;
	using System.Windows.Input;

	using Cirrious.CrossCore.Platform;
	using Cirrious.MvvmCross.Plugins.Sqlite;

	public class QueueEntity
	{
		public QueueEntity()
		{
			
		}

		public QueueEntity(IQueuedCommand command, IMvxJsonConverter jsonConverter)
		{
			this.CommandTypeName = command.GetType().FullName;
			this.SerializedObject = jsonConverter.SerializeObject(command);
		}

		public string CommandTypeName { get; set; }
		public string SerializedObject { get; set; }
	}

	public interface IQueuedCommand
	{
		void Execute();
	}

	public class WriteLineCommand : IQueuedCommand
	{
		public string Message { get; set; }
		public void Execute()
		{
			var x = Message;
		}
	}

	public class KickShannonsAssCommand : IQueuedCommand
	{
		public int TimesToKick { get; set; }
		public int TimesToLaugh { get; set; }
		public string InsultToSay { get; set; }
		public void Execute()
		{
		
		}
	}

	public class FirstViewModel : MvxViewModel
	{
		public FirstViewModel(ISQLiteConnectionFactory factory, IMvxJsonConverter jsonConverter)
		{
			var connection = factory.Create("queue.db");
			connection.CreateTable<QueueEntity>();
			
			var writeLineCommand = new WriteLineCommand { Message = "Test" };

			var queueEntity = new QueueEntity(writeLineCommand, jsonConverter);
			connection.Insert(queueEntity);

			var kickCommand = new KickShannonsAssCommand { InsultToSay = "You suck", TimesToKick = 10, TimesToLaugh = 20 };
			var kickEntity = new QueueEntity(kickCommand, jsonConverter);
			connection.Insert(kickEntity);

			var retrievedEntities = connection.Table<QueueEntity>().ToList();

			foreach (var retrievedEntity in retrievedEntities)
			{
				if (retrievedEntity != null)
				{
					var type = Type.GetType(retrievedEntity.CommandTypeName);
					var command = jsonConverter.DeserializeObject(type, retrievedEntity.SerializedObject);
					(command as IQueuedCommand).Execute();
				}
			}

		}

		public string Hello { get; set; }
	}
}
