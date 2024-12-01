using System;
using System.Collections;
using UnityEngine;


namespace THEBADDEST.Coroutines
{
	
	public class CoroutineHandler
	{

		static MonoRunner mono;
		/// <summary>
		/// Creates a new <see cref="MonoRunner"/> instance to run all coroutines on.
		/// </summary>
		/// <remarks>
		/// This method is called by Unity's RuntimeInitializeOnLoadMethod attribute.
		/// </remarks>
		[RuntimeInitializeOnLoadMethod]
		public static void CreateCoroutineHandler()
		{
			CoroutineHandler coroutineHandler = new CoroutineHandler();
			coroutineHandler.CreateMonoRunner();
		}

		/// <summary>
		/// Creates a new instance of <see cref="MonoRunner"/> and creates a new <see cref="CoroutineUpdate"/> to update it.
		/// </summary>
		/// <remarks>
		/// This method is called by the <see cref="CreateCoroutineHandler"/> method.
		/// </remarks>
		private void CreateMonoRunner()
		{
			GameObject monoRunnerObject = new GameObject("MonoRunner") { hideFlags = HideFlags.HideInHierarchy };
			mono                 = monoRunnerObject.AddComponent<MonoRunner>();
			updator = new CoroutineUpdate(mono);
		}
		#region Simple Corotine functions.......................
		

		/// <summary>
		/// Starts a coroutine on the <see cref="MonoRunner"/> instance.
		/// </summary>
		/// <param name="coroutine">The coroutine to start.</param>
		/// <returns>The coroutine that was started.</returns>
		public static UnityEngine.Coroutine StartStaticCoroutine(IEnumerator coroutine)
		{
			return mono.StartCoroutine(coroutine);
		}

/// <summary>
/// Stops the specified coroutine on the <see cref="MonoRunner"/> instance.
/// </summary>
/// <param name="coroutine">The coroutine to stop.</param>
		public static void StopStaticCoroutine(UnityEngine.Coroutine coroutine)
		{
			mono.StopCoroutine(coroutine);
		}
		

		/// <summary>
		/// Stops all coroutines on the <see cref="MonoRunner"/> instance.
		/// </summary>
		public static void StopAll()
		{
			mono.StopAllCoroutines();
		}

		#endregion


		#region Update With Coroutine

		static CoroutineUpdate updator;

		public static void DoUpdate(CoroutineMethod action, float delay = 0)
		{
			if (mono)
				updator.DoUpdate(action, delay);
		}

		public static void RemoveUpdate(CoroutineMethod action, float delay = 0)
		{
			if (mono)
				updator.RemoveUpdate(action, delay);
		}

		public static void StopUpdate(float delay = 0)
		{
			if (mono)
				updator.StopUpdate(delay);
		}

		#endregion


		#region AfterWait for single delay

		/// <summary>
		/// Starts a coroutine that waits for a specified number of seconds
		/// and then calls the specified action.
		/// </summary>
		/// <param name="action">The action to call after the wait.</param>
		/// <param name="seconds">The number of seconds to wait.</param>
		/// <param name="realTime">If true, the wait time is real-time, otherwise it is game-time.</param>
		/// <returns>The coroutine that was started.</returns>
		public static UnityEngine.Coroutine AfterWait(CoroutineMethod action, float seconds, bool realTime = false)
		{
			return mono.StartCoroutine(AfterWaitCoroutine(action, seconds, realTime));
		}

		/// <summary>
		/// Starts a coroutine that waits for a specified number of seconds
		/// and then calls the specified action.
		/// </summary>
		/// <param name="mono">The MonoBehaviour that the coroutine is attached to.</param>
		/// <param name="action">The action to call after the wait.</param>
		/// <param name="seconds">The number of seconds to wait.</param>
		/// <param name="realTime">If true, the wait time is real-time, otherwise it is game-time.</param>
		/// <returns>The coroutine that was started.</returns>
		public static UnityEngine.Coroutine AfterWait(MonoBehaviour mono, CoroutineMethod action, float seconds, bool realTime = false)
		{
			return mono.StartCoroutine(AfterWaitCoroutine(action, seconds, realTime));
		}

		static IEnumerator AfterWaitCoroutine(CoroutineMethod action, float seconds, bool realTime)
		{
			yield return new CoroutineDelay(action, seconds, realTime).Behaviour();
		}

		#endregion


		#region AfterWait for multipule functions

		public static UnityEngine.Coroutine AfterWait(params CoroutineSequence[] sequences)
		{
			return mono.StartCoroutine(AfterWaitCoroutine(sequences));
		}

		public static UnityEngine.Coroutine AfterWait(MonoBehaviour mono, params CoroutineSequence[] sequences)
		{
			return mono.StartCoroutine(AfterWaitCoroutine(sequences));
		}

		private static IEnumerator AfterWaitCoroutine(CoroutineSequence[] sequences)
		{
			foreach (var item in sequences)
			{
				yield return item.Behaviour();
			}
		}

		#endregion


		#region AfterWait for with condition

		public static UnityEngine.Coroutine AfterWait(CoroutineMethod action, Func<bool> condition)
		{
			return mono.StartCoroutine(AfterWaitCoroutine(action, condition));
		}

		public static UnityEngine.Coroutine AfterWait(MonoBehaviour mono, CoroutineMethod action, Func<bool> condition)
		{
			return mono.StartCoroutine(AfterWaitCoroutine(action, condition));
		}

		static IEnumerator AfterWaitCoroutine(CoroutineMethod action, Func<bool> condition)
		{
			yield return new CoroutineCondition(action, condition).Behaviour();
		}

		#endregion


		#region simpleWaitLoop

		public static UnityEngine.Coroutine WaitLoop(CoroutineMethod action, Func<bool> condition, float seconds = 0, bool realTime = false)
		{
			return mono.StartCoroutine(WaitLoopCoroutine(action, condition, seconds, realTime));
		}

		public static UnityEngine.Coroutine WaitLoop(MonoBehaviour mono, CoroutineMethod action, Func<bool> condition, float seconds = 0, bool realTime = false)
		{
			return mono.StartCoroutine(WaitLoopCoroutine(action, condition, seconds, realTime));
		}

		static IEnumerator WaitLoopCoroutine(CoroutineMethod action, Func<bool> condition, float seconds, bool realTime)
		{
			CoroutineDelay delay              = new CoroutineDelay(action, seconds, realTime);
			var            waitForFixedUpdate = new WaitForFixedUpdate();
			while (!condition())
			{
				yield return waitForFixedUpdate;
				if (seconds == 0)
					action.Invoke();
				else
					yield return delay.Behaviour();
			}
		}

		#endregion

	}

	


	


}

