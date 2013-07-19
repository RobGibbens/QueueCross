using System;
using System.Linq;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Plugins.Sqlite;

namespace Queue.Core.ViewModels
{
	using System.IO;

	using Newtonsoft.Json;

	public class MvxQueue : IMvxQueue
	{
		private readonly IMvxJsonConverter _jsonConverter;
		private ISQLiteConnection _connection;
		public MvxQueue(ISQLiteConnectionFactory factory, IMvxJsonConverter jsonConverter)
		{
			_jsonConverter = jsonConverter;
			_connection = factory.Create("queue.db");
			_connection.CreateTable<QueueEntity>();
		}

		public void Push(IQueuedCommand command)
		{
			var queueEntity = new QueueEntity(command, _jsonConverter);
			_connection.Insert(queueEntity);
		}

		public void ProcessQueue()
		{
			var retrievedEntities = _connection.Table<QueueEntity>().ToList();

			foreach (var retrievedEntity in retrievedEntities)
			{
				if (retrievedEntity != null)
				{
					var type = Type.GetType(retrievedEntity.CommandTypeName);
					var serializer = new Newtonsoft.Json.JsonSerializer();
					TextReader textReader = new StringReader(retrievedEntity.SerializedObject);
					JsonReader jsonReader = new JsonTextReader(textReader);
					IQueuedCommand command = (IQueuedCommand)serializer.Deserialize(jsonReader, type);
					command.Execute();
				}
			}
		}
	}
}