using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameDevUtils.Coroutine
{


	public class CoroutineUpdate
	{

		Dictionary<float, UpdateData> updateCoroutines = new Dictionary<float, UpdateData>();
		MonoBehaviour                 mono;

		public CoroutineUpdate(MonoBehaviour mono)
		{
			this.mono = mono;
		}

		public void DoUpdate(Action action, float delay)
		{
			if (updateCoroutines.TryGetValue(delay, out UpdateData result))
			{
				result.behavior.action += action;
				if (result.coroutine == null)
					result.coroutine = mono.StartCoroutine(DoUpdateCoroutine(result.behavior));
			}
			else
			{
				UpdateData data = new UpdateData {behavior = new CoroutineDelay(action, delay)};
				data.coroutine = mono.StartCoroutine(DoUpdateCoroutine(data.behavior));
				updateCoroutines.Add(delay, data);
			}
		}

		public void RemoveUpdate(Action action, float delayRegistor)
		{
			if (updateCoroutines.TryGetValue(delayRegistor, out UpdateData result))
			{
				result.behavior.action -= action;
				if (result.behavior.action == null)
				{
					mono.StopCoroutine(result.coroutine);
					updateCoroutines.Remove(delayRegistor);
				}
			}
		}

		public void StopUpdate(float delayRegistor)
		{
			if (updateCoroutines.TryGetValue(delayRegistor, out UpdateData result))
			{
				mono.StopCoroutine(result.coroutine);
				updateCoroutines.Remove(delayRegistor);
			}
		}

		IEnumerator DoUpdateCoroutine(CoroutineDelay behavior)
		{
			WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
			while (true)
			{
				yield return waitForFixedUpdate;
				yield return behavior.Behaviour();
			}
		}

		struct UpdateData
		{

			public CoroutineDelay        behavior;
			public UnityEngine.Coroutine coroutine;

		}

	}


}