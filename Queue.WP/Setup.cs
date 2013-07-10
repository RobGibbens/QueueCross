using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.WindowsPhone.Platform;
using Microsoft.Phone.Controls;
using Queue.Core.ViewModels;

namespace Queue.WP
{
	public class Setup : MvxPhoneSetup
	{
		public Setup(PhoneApplicationFrame rootFrame)
			: base(rootFrame)
		{
		}

		protected override IMvxApplication CreateApp()
		{
			Mvx.RegisterType<IMvxQueue, MvxQueue>();
			return new Core.App();
		}

		protected override IMvxTrace CreateDebugTrace()
		{
			return new DebugTrace();
		}
	}
}