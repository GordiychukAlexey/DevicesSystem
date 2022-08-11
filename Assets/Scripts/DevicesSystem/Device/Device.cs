using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using Tools.ActionsQueue;
using UniRx;
using UnityEngine;

namespace DevicesSystem.Device {
	public class Device : BaseDisposable {
		private readonly DeviceModel _model;
		private readonly DeviceView _view;

		public DeviceModel Model => _model;

		public IReactiveProperty<Vector3> State => _model.State;

		public Vector3 TargetState{ get; set; }
		
		private readonly ActionsQueue _stateActionsQueue;

		public ActionsQueue.CommandType CurrentCommandType{
			get => _stateActionsQueue.CurrentCommandType;
			set => _stateActionsQueue.CurrentCommandType = value;
		}

		private readonly Dictionary<Type, DeviceBahaviour> _behaviours = new Dictionary<Type, DeviceBahaviour>();

		public IReadOnlyDictionary<Type, DeviceBahaviour> Behaviours => _behaviours;

		public Device(DeviceModel model, DeviceView view){
			_model = model;
			_view = view;

			TargetState = _model.State.Value;

			_stateActionsQueue = new ActionsQueue(view);

			AddDisposable(model.CurrentCommandType.Subscribe(type => _stateActionsQueue.CurrentCommandType = type));

			AddDisposable(model.State.Subscribe(state => _view.transform.position = state));

			foreach (IDeviceBahaviourModel deviceBahaviourModel in model.Behaviours){
				var behaviour = this.CreateBehaviour(deviceBahaviourModel);
				_behaviours.Add(behaviour.GetType(), behaviour);
			}
		}

		public bool IsCanDo<TBehaviorType>(out TBehaviorType casted) where TBehaviorType : DeviceBahaviour{
			var isCan = _behaviours.TryGetValue(typeof(TBehaviorType), out var x);
			if (isCan){
				casted = x as TBehaviorType;
			} else{
				casted = null;
			}

			return isCan;
		}

		public void DoNewAction(IEnumerator action){
			try{
				_stateActionsQueue.DoNewAction(action);
			}
			catch (Exception e){
				TargetState = _model.State.Value;
				throw e;
			}
		}
	}
}