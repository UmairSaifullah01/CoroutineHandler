using System;
using System.Collections;
using UnityEngine;


namespace GameDevUtils.Coroutine
{


	/// <summary>
	/// This class allows us to start Coroutines from Non-Monobehaviour scripts.
	/// It has all the functions for coroutine actions e.g. a delayed call back method => AfterWait()
	/// </summary>
	

	public class CoroutineHandler : SingletonPersistent<CoroutineHandler>
	{

		#region Simple Corotine functions.......................

		protected override void Awake()
		{
			base.Awake();
			updator = new CoroutineUpdate(this);
		}

		public static UnityEngine.Coroutine StartStaticCoroutine(IEnumerator coroutine)
		{
			return Instance.StartCoroutine(coroutine);
		}

		public static void StopStaticCoroutine(UnityEngine.Coroutine coroutine)
		{
			Instance.StopCoroutine(coroutine);
		}
		

		public static void StopAll()
		{
			Instance.StopAllCoroutines();
		}

		#endregion


		#region Update With Coroutine

		static CoroutineUpdate updator;

		public static void DoUpdate(Action action, float delay = 0)
		{
			if (Instance)
				updator.DoUpdate(action, delay);
		}

		public static void RemoveUpdate(Action action, float delay = 0)
		{
			if (Instance)
				updator.RemoveUpdate(action, delay);
		}

		public static void StopUpdate(float delay = 0)
		{
			if (Instance)
				updator.StopUpdate(delay);
		}

		#endregion


		#region AfterWait for single delay

		public static UnityEngine.Coroutine AfterWait(Action action, float seconds, bool realTime = false)
		{
			return Instance.StartCoroutine(AfterWaitCoroutine(action, seconds, realTime));
		}

		public static UnityEngine.Coroutine AfterWait(MonoBehaviour Obj, Action action, float seconds, bool realTime = false)
		{
			return Obj.StartCoroutine(AfterWaitCoroutine(action, seconds, realTime));
		}

		static IEnumerator AfterWaitCoroutine(Action action, float seconds, bool realTime)
		{
			yield return new CoroutineDelay(action, seconds, realTime).Behaviour();
		}

		#endregion


		#region AfterWait for multipule functions

		public static UnityEngine.Coroutine AfterWait(params CoroutineSequence[] sequences)
		{
			return Instance.StartCoroutine(AfterWaitCoroutine(sequences));
		}

		public static UnityEngine.Coroutine AfterWait(MonoBehaviour Obj, params CoroutineSequence[] sequences)
		{
			return Obj.StartCoroutine(AfterWaitCoroutine(sequences));
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

		public static UnityEngine.Coroutine AfterWait(Action action, Func<bool> condition)
		{
			return Instance.StartCoroutine(AfterWaitCoroutine(action, condition));
		}

		public static UnityEngine.Coroutine AfterWait(MonoBehaviour Obj, Action action, Func<bool> condition)
		{
			return Obj.StartCoroutine(AfterWaitCoroutine(action, condition));
		}

		static IEnumerator AfterWaitCoroutine(Action action, Func<bool> condition)
		{
			yield return new CoroutineCondition(action, condition).Behaviour();
		}

		#endregion


		#region simpleWaitLoop

		public static UnityEngine.Coroutine WaitLoop(Action action, Func<bool> condition, float seconds = 0, bool realTime = false)
		{
			return Instance.StartCoroutine(WaitLoopCoroutine(action, condition, seconds, realTime));
		}

		public static UnityEngine.Coroutine WaitLoop(MonoBehaviour Obj, Action action, Func<bool> condition, float seconds = 0, bool realTime = false)
		{
			return Obj.StartCoroutine(WaitLoopCoroutine(action, condition, seconds, realTime));
		}

		static IEnumerator WaitLoopCoroutine(Action action, Func<bool> condition, float seconds, bool realTime)
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

