using UnityEngine;

namespace Ui {
	public class DevicesSystemUiView : MonoBehaviour {
		[SerializeField]private DevicesPanelUiView devicesPanelUiView;
		[SerializeField]private DevicePanelUiView devicePanelUiViewPrefab;
		[SerializeField]private AnalogDeviceBehaviorPanel analogDeviceBehaviorPanelPrefab;
		[SerializeField]private DiscreteDeviceBehaviorPanel discreteDeviceBehaviorPanelPrefab;

		public DevicesPanelUiView DevicesPanelUiView => devicesPanelUiView;
		public DevicePanelUiView DevicePanelUiViewPrefab => devicePanelUiViewPrefab;
		public AnalogDeviceBehaviorPanel AnalogDeviceBehaviorPanelPrefab => analogDeviceBehaviorPanelPrefab;
		public DiscreteDeviceBehaviorPanel DiscreteDeviceBehaviorPanelPrefab => discreteDeviceBehaviorPanelPrefab;
	}
}