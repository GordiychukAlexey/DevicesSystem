using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui {
	public class DevicesPanelUiView :MonoBehaviour {
		[SerializeField] private TMP_Dropdown _devicesDropdown;
		[SerializeField] private ToggleGroup _newDeviceBehaviorToggleGroup;
		[SerializeField] private Toggle _newDeviceDiscreteBehaviorToggle;
		[SerializeField] private Toggle _newDeviceAnalogBehaviorToggle;
		[SerializeField] private Button _newDeviceCreationButton;
		[SerializeField] private RectTransform _devicePanelRoot;

		public TMP_Dropdown DevicesDropdown => _devicesDropdown;
		public ToggleGroup NewDeviceBehaviorToggleGroup => _newDeviceBehaviorToggleGroup;
		public Toggle NewDeviceDiscreteBehaviorToggle => _newDeviceDiscreteBehaviorToggle;
		public Toggle NewDeviceAnalogBehaviorToggle => _newDeviceAnalogBehaviorToggle;
		public Button NewDeviceCreationButton => _newDeviceCreationButton;
		public RectTransform DevicePanelRoot => _devicePanelRoot;
	}
}