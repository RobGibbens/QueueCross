using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirrious.MvvmCross.Plugins.Json;
using Cirrious.MvvmCross.Plugins.Sqlite;
using Moq;
using NUnit.Framework;
using Queue.Core.ViewModels;
using Should;
using SQLite;

namespace Queue.Tests
{
	[TestFixture]
	public class QueueTests
	{
		[SetUp]
		public void Setup()
		{
			
		}

		[Test]
		public void Should_process_successful_children()
		{
			var factory = new Cirrious.MvvmCross.Plugins.Sqlite.Wpf.MvxWpfSqLiteConnectionFactory();
			var connection = factory.Create("queue.db");
			connection.CreateTable<QueueEntity>();
			var entities = connection.Table<QueueEntity>().ToList();
			foreach (var entity in entities)
			{
				connection.Delete(entity);
			}

			var jsonConverter = new MvxJsonConverter();
			IMvxQueue queue = new MvxQueue(factory, jsonConverter);
			var saveToLocalDatabaseCommand = new SaveToLocalDatabaseCommand { Message = "Test" };
			var saveToRemoteServiceCommand = new FakeCommand();
			
			saveToLocalDatabaseCommand.AddSuccessCommand(saveToRemoteServiceCommand);
			queue.Push(saveToLocalDatabaseCommand);
			queue.ProcessQueue();
		}
	}
}
