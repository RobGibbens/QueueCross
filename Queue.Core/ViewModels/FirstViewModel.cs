using Cirrious.MvvmCross.ViewModels;

namespace Queue.Core.ViewModels
{
	using System.Windows.Input;

	public class FirstViewModel : MvxViewModel
	{
		public FirstViewModel(IMvxQueue queue)
		{
			var saveToLocalDatabaseCommand = new SaveToLocalDatabaseCommand { Message = "Test" };
			var saveToRemoteServiceCommand = new SaveToRemoteServiceCommand { InsultToSay = "You suck", TimesToKick = 10, TimesToLaugh = 20 };
			saveToLocalDatabaseCommand.AddSuccessCommand(saveToRemoteServiceCommand);
			queue.Push(saveToLocalDatabaseCommand);
			queue.ProcessQueue();

		}

		public string Hello { get; set; }
	}
}
