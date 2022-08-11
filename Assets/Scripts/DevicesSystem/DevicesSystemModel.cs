using DevicesSystem.Device;
using Newtonsoft.Json;
using UniRx;

namespace DevicesSystem {
	public class DevicesSystemModel {
		[JsonProperty] public ReactiveCollection<DeviceModel> Devices = new ReactiveCollection<DeviceModel>();
	}
}