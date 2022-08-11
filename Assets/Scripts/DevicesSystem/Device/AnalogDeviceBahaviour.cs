using UnityEngine;

namespace DevicesSystem.Device {
	public class AnalogDeviceBahaviour : DeviceBahaviour {
		private readonly AnalogDeviceBahaviourModel _model;

		private readonly AnalogVectorStateChanger analogVectorStateChanger;

		public float StateChangeSpeed{
			get => _model.StateChangeSpeed;
			set => _model.StateChangeSpeed = value;
		}

		public AnalogDeviceBahaviour(Device device, AnalogDeviceBahaviourModel model) : base(device, model, typeof(AnalogDeviceBahaviour)){
			_model = model;

			analogVectorStateChanger = new AnalogVectorStateChanger();
		}

		public override void ChangeState(Vector3 targetState){
			Device.DoNewAction(
				analogVectorStateChanger.ChangeState(
					Device.State,
					new AnalogVectorStateChangeParameters(targetState, _model.StateChangeSpeed)));
		}
	}
}