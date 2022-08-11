using System;
using Tools.ActionsQueue;
using UnityEngine;

namespace DevicesSystem.Device {
	public abstract class DeviceBahaviour {
		private readonly Device _device;
		private readonly IDeviceBahaviourModel _model;

		protected Device Device => _device;

		public IDeviceBahaviourModel Model => _model;

		protected DeviceBahaviour(Device device, IDeviceBahaviourModel model, Type behaviorType){
			_device = device;
			_model = model;
		}

		public abstract void ChangeState(Vector3 targetState);

		public void Move(Vector3 vector){
			switch (_device.CurrentCommandType){
				case ActionsQueue.CommandType.Force:
				case ActionsQueue.CommandType.RunOrException:
					_device.TargetState = _device.State.Value + vector;
					break;
				case ActionsQueue.CommandType.Append:
					_device.TargetState += vector;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			ChangeState(_device.TargetState);
		}
	}
}