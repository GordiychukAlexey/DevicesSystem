using System;
using System.Collections;
using UnityEngine;

namespace Tests.PlayModeTests {
	public class TestRoutineRunner : MonoBehaviour {
		private static TestRoutineRunner instance;

		public static TestRoutineRunner Instance{
			get{
				if (instance) return instance;

				instance = FindObjectOfType<TestRoutineRunner>();

				if (instance) return instance;

				instance = new GameObject("TestRoutineRunner").AddComponent<TestRoutineRunner>();

				return instance;
			}
		}

		public void TestRoutine(IEnumerator routine, Action whenDone){
			StartCoroutine(RunRoutine(routine, whenDone));
		}

		private IEnumerator RunRoutine(IEnumerator routine, Action whenDone){
			yield return routine;

			whenDone?.Invoke();
		}
	}
}