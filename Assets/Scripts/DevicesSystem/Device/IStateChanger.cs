using System.Collections;
using UniRx;

namespace DevicesSystem.Device {
	public interface IStateChanger<TStateType, in TStateChangeParams> {
		IEnumerator ChangeState(IReactiveProperty<TStateType> state, TStateChangeParams stateChangeParams);
	}

	public interface IStateChanger<in TStateChangeParams> {
		IEnumerator ChangeState(Device device, TStateChangeParams stateChangeParams);
	}
}