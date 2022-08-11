using System;

namespace Tools.ActionsQueue {
	public class ActionsQueueException : Exception {
		public ActionsQueueException(string e) : base(e){ }
	}
}