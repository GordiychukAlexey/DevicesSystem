using System.Collections.Generic;
using Newtonsoft.Json;
using Tools.ActionsQueue;
using UniRx;
using UnityEngine;

namespace DevicesSystem.Device {
	public class DeviceModel {
		[JsonProperty]
		public ReactiveProperty<Vector3> State{ get; set; }

		[JsonProperty]
		public ReactiveProperty<ActionsQueue.CommandType> CurrentCommandType{ get; set; } =
			new ReactiveProperty<ActionsQueue.CommandType>(ActionsQueue.CommandType.Append);

		[JsonProperty]
		public List<IDeviceBahaviourModel> Behaviours{ get; set; } = new List<IDeviceBahaviourModel>();
	}
}