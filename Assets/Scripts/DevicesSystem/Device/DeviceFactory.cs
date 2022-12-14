using UnityEngine;

namespace DevicesSystem.Device {
	public class DeviceFactory {
		private DeviceView _deviceViewPrefab;

		public DeviceFactory(DeviceView deviceViewPrefab){
			_deviceViewPrefab = deviceViewPrefab;
		}

		public Device Create(DeviceModel deviceModel){
			DeviceView deviceViewInstance = Object.Instantiate(_deviceViewPrefab);

			Device newDevice = new Device(deviceModel, deviceViewInstance);

			return newDevice;
		}
	}
}