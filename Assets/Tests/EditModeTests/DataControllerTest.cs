using System.Collections.Generic;
using DevicesSystem;
using DevicesSystem.Device;
using NUnit.Framework;
using Tools.ActionsQueue;
using UniRx;
using UnityEngine;

namespace Tests.EditModeTests {
	public class DataControllerTest {
		[Test]
		public void Test(){
			DataController dataController = new DataController();

			var devicesSystemModel = GetDefaultDevicesSystemModel();

			dataController.Save(devicesSystemModel);

			var loadedDevicesSystemModel = dataController.Load();

			for (int i = 0; i < devicesSystemModel.Devices.Count; i++){
				Assert.That(devicesSystemModel.Devices[i].Behaviours.Count == loadedDevicesSystemModel.Devices[i].Behaviours.Count);
			}
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
	}
}