using UnityEngine;

namespace DevicesSystem.Device {
	public class DiscreteDeviceBahaviour : DeviceBahaviour {
		private DiscreteVectorStateChanger discreteVectorStateChanger;

		public DiscreteDeviceBahaviour(Device device, DiscreteDeviceBahaviourModel model) : base(device, model){
			discreteVectorStateChanger = new DiscreteVectorStateChanger();
		}

		public override void ChangeState(Vector3 targetState){
			Device.DoNewAction(
				discreteVectorStateChanger.ChangeState(
					Device.State,
					targetState));
		}
	}
}