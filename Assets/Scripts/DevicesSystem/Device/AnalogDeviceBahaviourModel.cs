using Newtonsoft.Json;

namespace DevicesSystem.Device {
	public class AnalogDeviceBahaviourModel : IDeviceBahaviourModel {
		[JsonProperty]
		public float StateChangeSpeed{ get; set; }
	}
}