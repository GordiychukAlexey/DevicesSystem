using System.Collections;
using UniRx;
using UnityEngine;

namespace DevicesSystem.Device {
	public class DiscreteVectorStateChanger : IStateChanger<Vector3, Vector3> {
		public IEnumerator ChangeState(IReactiveProperty<Vector3> state, Vector3 targetState){
			state.Value = targetState;
			yield break;
		}
	}
}