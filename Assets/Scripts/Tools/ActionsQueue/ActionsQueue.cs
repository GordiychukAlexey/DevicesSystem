using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools.ActionsQueue {
	public class ActionsQueue {
		private readonly MonoBehaviour coroutiner;

		private readonly Queue<IEnumerator> actions = new Queue<IEnumerator>();

		public CommandType CurrentCommandType{ get; set; } = CommandType.Append;

		private Coroutine currentCoroutine;

		public enum CommandType {
			Force,
			RunOrException,
			Append,
		}

		public ActionsQueue(MonoBehaviour coroutiner){
			this.coroutiner = coroutiner;
		}

		public void DoNewAction(IEnumerator action){
			DoNewAction(action, CurrentCommandType);
		}

		private void DoNewAction(IEnumerator action, CommandType commandType){
			switch (commandType){
				case CommandType.Force:
					StopCurrentActions();

					StartNewCoroutine(action);
					break;
				case CommandType.RunOrException:
					if (currentCoroutine != null){
						throw new ActionsQueueException("Wrong attempt to do new action");
					} else{
						StartNewCoroutine(action);
					}

					break;
				case CommandType.Append:
					if (currentCoroutine != null){
						actions.Enqueue(action);
					} else{
						StartNewCoroutine(action);
					}

					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(commandType), commandType, null);
			}
		}

		private void StopCurrentActions(){
			if (currentCoroutine != null){
				coroutiner.StopCoroutine(currentCoroutine);
				currentCoroutine = null;
			}

			actions.Clear();
		}

		private void StartNewCoroutine(IEnumerator action){
			actions.Enqueue(action);
			currentCoroutine = coroutiner.StartCoroutine(RunQueue());
		}

		private IEnumerator RunQueue(){
			while (actions.TryDequeue(out var action)){
				yield return action;
			}

			currentCoroutine = null;
		}
	}
}