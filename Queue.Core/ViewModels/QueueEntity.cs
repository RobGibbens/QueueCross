using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Plugins.Sqlite;

namespace Queue.Core.ViewModels
{
	public class QueueEntity
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
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
}