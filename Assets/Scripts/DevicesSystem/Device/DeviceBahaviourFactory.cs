using System;

namespace DevicesSystem.Device {
	public static class DeviceBahaviourFactory {
		public static DeviceBahaviour CreateBehaviour(this Device device, IDeviceBahaviourModel bahaviourModel){
			return bahaviourModel switch{
				DiscreteDeviceBahaviourModel discreteDeviceBahaviourModel => new DiscreteDeviceBahaviour(device, discreteDeviceBahaviourModel),
				AnalogDeviceBahaviourModel analogDeviceBahaviourModel     => new AnalogDeviceBahaviour(device, analogDeviceBahaviourModel),
				_                                                         => throw new ArgumentOutOfRangeException(nameof(bahaviourModel))
			};
		}
	}
}