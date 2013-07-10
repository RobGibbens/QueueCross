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
			//var factory = new Mock<ISQLiteConnectionFactory>();
			//var connection = new Mock<ISQLiteConnection>();

			//var table = new Mock<ITableQuery<QueueEntity>>();
			//table.SetupAllProperties();
			//connection.Setup(x => x.Table<QueueEntity>()).Returns(table.Object);
			//factory.Setup(x => x.Create(It.IsAny<string>())).Returns(connection.Object);
			var jsonConverter = new MvxJsonConverter();
			IMvxQueue queue = new MvxQueue(factory, jsonConverter);
			var saveToLocalDatabaseCommand = new SaveToLocalDatabaseCommand { Message = "Test" };
			var saveToRemoteServiceCommand = new FakeCommand();
			//var saveToRemoteServiceCommand = new SaveToRemoteServiceCommand { InsultToSay = "You suck", TimesToKick = 10, TimesToLaugh = 20 };
			saveToLocalDatabaseCommand.AddSuccessCommand(saveToRemoteServiceCommand);
			queue.Push(saveToLocalDatabaseCommand);
			queue.ProcessQueue();
			saveToRemoteServiceCommand.ExecuteWasCalled2.ShouldBeTrue();
		}
	}
}
