using System;
using UnityEngine;


namespace GameDevUtils.Coroutine
{


	public static class CoroutineExtensions
	{

		public static UnityEngine.Coroutine AfterWait(this MonoBehaviour Obj, Action action, float seconds, bool realTime = false)
		{
			return CoroutineHandler.AfterWait(Obj, action, seconds, realTime);
		}

		public static UnityEngine.Coroutine AfterWait(this MonoBehaviour Obj, params CoroutineSequence[] sequences)
		{
			return CoroutineHandler.AfterWait(Obj, sequences);
		}

		public static UnityEngine.Coroutine AfterWait(this MonoBehaviour Obj, Action action, Func<bool> condition)
		{
			return CoroutineHandler.AfterWait(Obj, action, condition);
		}

		public static UnityEngine.Coroutine WaitLoop(this MonoBehaviour Obj, Action action, Func<bool> condition, float seconds = 0, bool realTime = false)
		{
			return CoroutineHandler.WaitLoop(Obj, action, condition, seconds, realTime);
		}

	}


}