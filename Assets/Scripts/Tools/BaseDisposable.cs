using System;
using System.Collections.Generic;

namespace Tools {
	public abstract class BaseDisposable : IDisposable {
		private readonly List<IDisposable> _disposables = new List<IDisposable>();

		public T AddDisposable<T>(T disposable) where T : IDisposable{
			_disposables.Add(disposable);
			return disposable;
		}
	
		public T RemoveDisposable<T>(T disposable) where T : IDisposable{
			_disposables.Remove(disposable);
			return disposable;
		}

		public void Dispose(){
			foreach (IDisposable disposable in _disposables){
				disposable.Dispose();
			}
		}
	}
}