using System.Collections.Generic;
using DevicesSystem;
using DevicesSystem.Device;
using Tools.ActionsQueue;
using Ui;
using UniRx;
using UnityEngine;

public class SceneContext : MonoBehaviour {
	[SerializeField] private DeviceView _deviceViewPrefab;

	[SerializeField] private DevicesSystemUiView _devicesSystemUiView;

	private DevicesSystem.DevicesSystem devicesSystem;

	private DevicesSystemUiController devicesSystemUi;

	DataController dataController;

	void Start(){
		DeviceFactory deviceFactory = new DeviceFactory(_deviceViewPrefab);

		dataController = new DataController();

		dataController.Save(GetDefaultDevicesSystemModel());

		var loadedDevicesSystemModel = dataController.Load();

		devicesSystem = new DevicesSystem.DevicesSystem(loadedDevicesSystemModel, deviceFactory, this);

		devicesSystemUi = new DevicesSystemUiController(_devicesSystemUiView, devicesSystem);
	}

	private DevicesSystemModel GetDefaultDevicesSystemModel(){
		return new DevicesSystemModel(){
			Devices = new ReactiveCollection<DeviceModel>(){
				new DeviceModel(){
					State = new Vector3ReactiveProperty(Vector3.left * 5),
					CurrentCommandType = new ReactiveProperty<ActionsQueue.CommandType>(ActionsQueue.CommandType.Append),
					Behaviours = new List<IDeviceBahaviourModel>(){
						new AnalogDeviceBahaviourModel(){
							StateChangeSpeed = 5.0f,
						},
						new DiscreteDeviceBahaviourModel(){ }
					}
				},
				new DeviceModel(){
					State = new Vector3ReactiveProperty(Vector3.up * 5),
					CurrentCommandType = new ReactiveProperty<ActionsQueue.CommandType>(ActionsQueue.CommandType.Append),
					Behaviours = new List<IDeviceBahaviourModel>(){
						new AnalogDeviceBahaviourModel(){
							StateChangeSpeed = 5.0f,
						}
					}
				},
			}
		};
	}

	private void OnDestroy(){
		devicesSystem.Dispose();
		devicesSystemUi.Dispose();
	}
}