using System;
using System.Collections;
using UnityEngine;


namespace GameDevUtils.Coroutine
{


	[System.Serializable]
	public class CoroutineDelay : CoroutineSequence
	{

		public    float            seconds;
		protected bool             realTime = false;
		protected YieldInstruction iterator2;


		public CoroutineDelay(Action m_action, float m_seconds, bool m_realTime = false)
		{
			action   = m_action;
			seconds  = m_seconds;
			realTime = m_realTime;
			if (realTime)
				iterator   = new WaitForSecondsRealtime(seconds);
			else iterator2 = new WaitForSeconds(seconds);
		}

		public override IEnumerator Behaviour()
		{
			if (realTime)
				yield return iterator;
			else yield return iterator2;
			action.Invoke();
		}

	}


}