using System;
using UnityEngine;


namespace THEBADDEST.Coroutines
{


	public static class CoroutineExtensions
	{

		public static UnityEngine.Coroutine AfterWait(this MonoBehaviour mono, CoroutineMethod action, float seconds, bool realTime = false)
		{
			return CoroutineHandler.AfterWait(mono, action, seconds, realTime);
		}

		public static UnityEngine.Coroutine AfterWait(this MonoBehaviour mono, params CoroutineSequence[] sequences)
		{
			return CoroutineHandler.AfterWait(mono, sequences);
		}

		public static UnityEngine.Coroutine AfterWait(this MonoBehaviour mono, CoroutineMethod action, Func<bool> condition)
		{
			return CoroutineHandler.AfterWait(mono, action, condition);
		}

		public static UnityEngine.Coroutine WaitLoop(this MonoBehaviour mono, CoroutineMethod action, Func<bool> condition, float seconds = 0, bool realTime = false)
		{
			return CoroutineHandler.WaitLoop(mono, action, condition, seconds, realTime);
		}

	}


}