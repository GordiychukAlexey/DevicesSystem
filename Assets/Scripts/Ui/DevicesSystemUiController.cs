using System;
using System.Collections.Generic;
using System.Linq;
using DevicesSystem;
using DevicesSystem.Device;
using TMPro;
using Tools;
using Tools.ActionsQueue;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui {
	public class DevicesSystemUiController : BaseDisposable {
		private static readonly Dictionary<Type, (int, string)> _behaviourParametersByBehaviourType = new Dictionary<Type, (int, string)>(){
			{typeof(DiscreteDeviceBahaviour), (0, "Discrete")},
			{typeof(AnalogDeviceBahaviour), (1, "Analog")},
		};

		private static readonly Dictionary<ActionsQueue.CommandType, int> _commandIndexByCommandType =
			Enum.GetValues(typeof(ActionsQueue.CommandType))
				.Cast<ActionsQueue.CommandType>()
				.Select((type, i) => (type, i))
				.ToDictionary(tuple => tuple.type,
							  tuple => tuple.i);

		private readonly DevicesSystemUiView _devicesSystemUiView;
		private readonly DevicePanelUiView _devicePanelUiView;
		private readonly DevicesSystem.DevicesSystem _devicesSystem;


		private readonly Dictionary<int, (ActionsQueue.CommandType, string)> _commandParametersByIndex =
			_commandIndexByCommandType.ToDictionary(
				pair => pair.Value,
				pair => (pair.Key, pair.Key.ToString()));

		private readonly Dictionary<Type, DeviceBehaviorPanel> _deviceBahaviourPanelByBehaviourType;

		private Dictionary<int, DeviceBahaviour> _deviceBahaviourByIndex;

		private DeviceBahaviour _currentDeviceBahaviour;

		private Dictionary<int, Device> deviceByIndex;

		public DevicesSystemUiController(DevicesSystemUiView devicesSystemUiView, DevicesSystem.DevicesSystem devicesSystem){
			_devicesSystemUiView = devicesSystemUiView;
			_devicesSystem = devicesSystem;

			_devicePanelUiView = Object.Instantiate(
				_devicesSystemUiView.DevicePanelUiViewPrefab,
				_devicesSystemUiView.DevicesPanelUiView.transform);

			_deviceBahaviourPanelByBehaviourType = new Dictionary<Type, DeviceBehaviorPanel>(){
				{
					typeof(DiscreteDeviceBahaviour),
					Object.Instantiate(
						_devicesSystemUiView.DiscreteDeviceBehaviorPanelPrefab,
						_devicePanelUiView.DeviceBehaviorPanelRoot.transform)
				}, {
					typeof(AnalogDeviceBahaviour),
					Object.Instantiate(
						_devicesSystemUiView.AnalogDeviceBehaviorPanelPrefab,
						_devicePanelUiView.DeviceBehaviorPanelRoot.transform)
				},
			};

			_devicesSystemUiView.DevicesPanelUiView.DevicesDropdown.onValueChanged.RemoveAllListeners();
			_devicesSystemUiView.DevicesPanelUiView.DevicesDropdown.onValueChanged.AddListener(
				val => SetupNewDevice(deviceByIndex[val]));

			UpdateDevices();
			_devicesSystemUiView.DevicesPanelUiView.DevicesDropdown.SetValueAndCallback(0);


			_devicesSystemUiView.DevicesPanelUiView.NewDeviceCreationButton.onClick.RemoveAllListeners();
			_devicesSystemUiView.DevicesPanelUiView.NewDeviceCreationButton.onClick.AddListener(() => {
				CreateNewDevice();

				_devicesSystemUiView.DevicesPanelUiView.DevicesDropdown.SetValueAndCallback(
					_devicesSystemUiView.DevicesPanelUiView.DevicesDropdown.options.Count - 1);
			});

			_devicePanelUiView.CommandTypeDropdown.options =
				Enum.GetNames(typeof(ActionsQueue.CommandType))
					.Select(s => new TMP_Dropdown.OptionData(s))
					.ToList();

			AddDisposable(devicesSystem.Devices.ObserveAdd().Subscribe(ev => UpdateDevices()));
		}

		private void UpdateDevices(){
			deviceByIndex = _devicesSystem.Devices
										  .Select((model, i) => (model, i))
										  .ToDictionary(tuple => tuple.i, tuple => tuple.model);

			_devicesSystemUiView.DevicesPanelUiView.DevicesDropdown.options =
				deviceByIndex
				   .Select(pair => new TMP_Dropdown.OptionData(pair.Key.ToString()))
				   .ToList();
		}

		private void CreateNewDevice(){
			if (
				!_devicesSystemUiView.DevicesPanelUiView.NewDeviceDiscreteBehaviorToggle.isOn &&
				!_devicesSystemUiView.DevicesPanelUiView.NewDeviceAnalogBehaviorToggle.isOn){
				throw new Exception($"Impossible to create create Device without behaviour");
			}

			_devicesSystem.CreateDevice(
				_devicesSystemUiView.DevicesPanelUiView.NewDeviceDiscreteBehaviorToggle.isOn,
				_devicesSystemUiView.DevicesPanelUiView.NewDeviceAnalogBehaviorToggle.isOn);
		}

		private void SetupNewDevice(Device device){
			_devicePanelUiView.CommandTypeDropdown.onValueChanged.RemoveAllListeners();
			_devicePanelUiView.CommandTypeDropdown.onValueChanged.AddListener(
				val => device.Model.CurrentCommandType.Value = _commandParametersByIndex[val].Item1
			);

			_devicePanelUiView.CommandTypeDropdown.SetValueAndCallback(_commandIndexByCommandType[device.Model.CurrentCommandType.Value]);

			_deviceBahaviourByIndex = device.Behaviours
											.Select(behaviour => (behaviour, _behaviourParametersByBehaviourType[behaviour.Key]))
											.OrderBy(tuple => tuple.Item2.Item1)
											.Select((tuple, i) => (i, tuple))
											.ToDictionary(tuple => tuple.i,
														  tuple => tuple.tuple.Item1.Value);

			_devicePanelUiView.BehaviorTypeDropdown.options =
				_deviceBahaviourByIndex
				   .OrderBy(pair => pair.Key)
				   .Select(pair => _behaviourParametersByBehaviourType[pair.Value.GetType()])
				   .Select(tuple => new TMP_Dropdown.OptionData(tuple.Item2))
				   .ToList();

			foreach (KeyValuePair<Type, DeviceBahaviour> pair in device.Behaviours){
				SetupDeviceBahaviourPanel(pair.Value);
			}

			_devicePanelUiView.BehaviorTypeDropdown.onValueChanged.RemoveAllListeners();
			_devicePanelUiView.BehaviorTypeDropdown.onValueChanged.AddListener(
				val => {
					_currentDeviceBahaviour = _deviceBahaviourByIndex[val];
					EnableDeviceBahaviourPanel(_currentDeviceBahaviour);
				});

			_devicePanelUiView.BehaviorTypeDropdown.SetValueAndCallback(0);

			_devicePanelUiView.LeftButton.onClick.RemoveAllListeners();
			_devicePanelUiView.LeftButton.onClick.AddListener(() => CurrentDeviceBahaviourMove(() => _currentDeviceBahaviour, Vector3.left * 5));
			_devicePanelUiView.RightButton.onClick.RemoveAllListeners();
			_devicePanelUiView.RightButton.onClick.AddListener(() => CurrentDeviceBahaviourMove(() => _currentDeviceBahaviour, Vector3.right * 5));
			_devicePanelUiView.UpButton.onClick.RemoveAllListeners();
			_devicePanelUiView.UpButton.onClick.AddListener(() => CurrentDeviceBahaviourMove(() => _currentDeviceBahaviour, Vector3.up * 5));
			_devicePanelUiView.DownButton.onClick.RemoveAllListeners();
			_devicePanelUiView.DownButton.onClick.AddListener(() => CurrentDeviceBahaviourMove(() => _currentDeviceBahaviour, Vector3.down * 5));
		}

		private void CurrentDeviceBahaviourMove(Func<DeviceBahaviour> deviceBahaviour, Vector3 vector) => deviceBahaviour().Move(vector);

		private void SetupDeviceBahaviourPanel(DeviceBahaviour deviceBahaviour){
			switch (deviceBahaviour){
				case DiscreteDeviceBahaviour discreteDeviceBahaviour:{
					DiscreteDeviceBehaviorPanel discreteDeviceBehaviorPanel =
						(DiscreteDeviceBehaviorPanel) _deviceBahaviourPanelByBehaviourType[deviceBahaviour.GetType()];
				}
					break;
				case AnalogDeviceBahaviour analogDeviceBahaviour:{
					AnalogDeviceBehaviorPanel analogDeviceBehaviorPanel =
						(AnalogDeviceBehaviorPanel) _deviceBahaviourPanelByBehaviourType[deviceBahaviour.GetType()];

					analogDeviceBehaviorPanel.ApplySpeedButton.onClick.RemoveAllListeners();
					analogDeviceBehaviorPanel.ApplySpeedButton.onClick.AddListener(
						() => {
							if (float.TryParse(analogDeviceBehaviorPanel.SpeedInputField.text, out float newSpeed)){
								analogDeviceBahaviour.StateChangeSpeed = newSpeed;
							} else{
								throw new FormatException($"Wrong number {analogDeviceBehaviorPanel.SpeedInputField.text}");
							}
						});

					analogDeviceBehaviorPanel.SpeedInputField.text = $"{analogDeviceBahaviour.StateChangeSpeed:F1}";
				}
					break;
			}
		}

		private void EnableDeviceBahaviourPanel(DeviceBahaviour deviceBahaviour){
			var deviceBahaviourType = deviceBahaviour.GetType();
			foreach (KeyValuePair<Type, DeviceBehaviorPanel> pair in _deviceBahaviourPanelByBehaviourType){
				pair.Value.gameObject.SetActive(pair.Key == deviceBahaviourType);
			}
		}
	}
}