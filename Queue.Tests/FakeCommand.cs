﻿using System;
using System.Collections.Generic;
using Queue.Core.ViewModels;

namespace Queue.Tests
{
	public class FakeCommand : IQueuedCommand
	{
		public bool ExecuteWasCalled2 { get; set; }
		public void Execute()
		{
			this.ExecuteWasCalled2 = true;
		}

		public IEnumerable<IQueuedCommand> OnSuccessCommands { get; private set; }
		public void AddSuccessCommand(IQueuedCommand command)
		{
			throw new NotImplementedException();
		}

		public void RemoveSuccessCommand(IQueuedCommand command)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<IQueuedCommand> OnErrorCommands { get; private set; }
		public void AddErrorCommand(IQueuedCommand command)
		{
			throw new NotImplementedException();
		}

		public void RemoveErrorCommand(IQueuedCommand command)
		{
			throw new NotImplementedException();
		}
	}
}