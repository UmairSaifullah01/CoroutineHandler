using System;
using System.Collections;
using UnityEngine;


namespace GameDevUtils.Coroutine
{


	[System.Serializable]
	public class CoroutineCondition : CoroutineSequence
	{

		public CoroutineCondition(Action m_action, Func<bool> m_condition)
		{
			action   = m_action;
			iterator = new WaitUntil(m_condition);
		}

		public override IEnumerator Behaviour()
		{
			yield return iterator;
			action.Invoke();
		}

	}


}