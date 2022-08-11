using UnityEngine;

namespace DevicesSystem.Device {
	public readonly struct AnalogVectorStateChangeParameters {
		public Vector3 TargetState{ get; }
		public float StateChangeSpeed{ get; }

		public AnalogVectorStateChangeParameters(Vector3 targetState, float stateChangeSpeed){
			TargetState = targetState;
			StateChangeSpeed = stateChangeSpeed;
		}
	}
}