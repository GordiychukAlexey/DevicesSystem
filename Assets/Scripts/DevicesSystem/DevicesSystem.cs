using DevicesSystem.Device;
using Tools;
using Tools.ActionsQueue;
using UniRx;
using UnityEngine;

namespace DevicesSystem {
	public class DevicesSystem : BaseDisposable {
		private readonly DevicesSystemModel _model;
		private readonly DeviceFactory _deviceFactory;
		private readonly MonoBehaviour _coroutiner;

		private ReactiveCollection<Device.Device> _devices = new ReactiveCollection<Device.Device>();

		public IReadOnlyReactiveCollection<Device.Device> Devices => _devices;

		public DevicesSystem(DevicesSystemModel model, DeviceFactory deviceFactory, MonoBehaviour coroutiner){
			_model = model;
			_deviceFactory = deviceFactory;
			_coroutiner = coroutiner;

			foreach (DeviceModel deviceModel in _model.Devices){
				AddDevice(deviceModel);
			}

			AddDisposable(_model.Devices.ObserveAdd().Subscribe(ev => AddDevice(ev.Value)));
		}

		private void AddDevice(DeviceModel deviceModel){
			var newDevice = AddDisposable(_deviceFactory.Create(deviceModel, _coroutiner));
			_devices.Add(newDevice);
		}

		public void CreateDevice(DeviceModel deviceModel) => _model.Devices.Add(deviceModel);

		public void CreateDevice(in bool isDiscrete, in bool isAnalog){
			DeviceModel newDeviceModel = new DeviceModel(){
				State = new ReactiveProperty<Vector3>(Vector3.zero),
				CurrentCommandType = new ReactiveProperty<ActionsQueue.CommandType>(ActionsQueue.CommandType.Append),
			};

			if (isDiscrete){
				newDeviceModel.Behaviours.Add(new DiscreteDeviceBahaviourModel());
			}

			if (isAnalog){
				newDeviceModel.Behaviours.Add(new AnalogDeviceBahaviourModel(){
					StateChangeSpeed = 5.0f,
				});
			}

			CreateDevice(newDeviceModel);
		}
	}
}