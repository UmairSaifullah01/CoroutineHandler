using System;
using System.Collections.Generic;
using UnityEngine;


namespace GameDevUtils.Coroutine
{


	public class CoroutineBuilder
	{

		private static   CoroutineBuilder        instance;
		private          MonoBehaviour           monobehaviour;
		private readonly List<CoroutineSequence> sequences = new List<CoroutineSequence>();

		public static CoroutineBuilder Builder(MonoBehaviour behaviour)
		{
			instance = new CoroutineBuilder {monobehaviour = behaviour};
			return instance;
		}

		public UnityEngine.Coroutine Run()
		{
			return CoroutineHandler.AfterWait(monobehaviour, sequences.ToArray());
		}

		public CoroutineBuilder AfterWait(Action action, float seconds, bool realTime = false)
		{
			instance.sequences.Add(new CoroutineDelay(action, seconds, realTime));
			return instance;
		}

		public CoroutineBuilder AfterWait(Action action, Func<bool> condition)
		{
			instance.sequences.Add(new CoroutineCondition(action, condition));
			return instance;
		}
		//public FlowBehaviour WaitLoop (Action action, Func<bool> condition, float seconds = 0, bool realTime = false)
		//{

		//}

	}


}