using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui {
	public class AnalogDeviceBehaviorPanel : DeviceBehaviorPanel {
		[SerializeField] private TMP_InputField _speedInputField;
		[SerializeField] private Button _applySpeedButton;

		public TMP_InputField SpeedInputField => _speedInputField;
		public Button ApplySpeedButton => _applySpeedButton;
	}
}