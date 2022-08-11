using System.Collections;
using UniRx;
using UnityEngine;

namespace DevicesSystem.Device {
	public class AnalogVectorStateChanger : IStateChanger<Vector3, AnalogVectorStateChangeParameters> {
		public IEnumerator ChangeState(IReactiveProperty<Vector3> state, AnalogVectorStateChangeParameters parameters){
			while (state.Value != parameters.TargetState){
				state.Value = Vector3.MoveTowards(state.Value, parameters.TargetState, parameters.StateChangeSpeed * Time.deltaTime);
				yield return null;
			}
		}
	}
}