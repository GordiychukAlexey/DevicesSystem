using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui {
	public class DevicePanelUiView : MonoBehaviour {
		[SerializeField] private TMP_Dropdown _commandTypeDropdown;
		[SerializeField] private TMP_Dropdown _behaviorTypeDropdown;
		[SerializeField] private RectTransform _deviceBehaviorPanelRoot;
		
		[SerializeField] private Button _LeftButton;
		[SerializeField] private Button _RightButton;
		[SerializeField] private Button _UpButton;
		[SerializeField] private Button _DownButton;

		
		public TMP_Dropdown CommandTypeDropdown => _commandTypeDropdown;
		public TMP_Dropdown BehaviorTypeDropdown => _behaviorTypeDropdown;
		public RectTransform DeviceBehaviorPanelRoot => _deviceBehaviorPanelRoot;

		public Button LeftButton => _LeftButton;
		public Button RightButton => _RightButton;
		public Button UpButton => _UpButton;
		public Button DownButton => _DownButton;
	}
}