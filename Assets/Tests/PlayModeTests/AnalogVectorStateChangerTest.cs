using System.Collections;
using DevicesSystem;
using DevicesSystem.Device;
using NUnit.Framework;
using UniRx;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayModeTests {
	public class AnalogVectorStateChangerTest {
		[UnityTest]
		public IEnumerator Test(){
			ReactiveProperty<Vector3> state = new ReactiveProperty<Vector3>(Vector3.left);
			AnalogVectorStateChanger analogVectorStateChanger = new AnalogVectorStateChanger();

			Vector3 targetState = Vector3.right;

			float speed = 2;
			float distance = Vector3.Distance(state.Value, targetState);

			TestRoutineRunner.Instance.StartCoroutine(
				analogVectorStateChanger.ChangeState(state, new AnalogVectorStateChangeParameters(targetState, speed)));

			yield return new WaitForSeconds(distance / speed);
			
//			Debug.Log($"{state}");
			Assert.That(state.Value == targetState);
		}
	}
}