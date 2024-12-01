using System;
using System.Collections;
using UnityEngine;


namespace THEBADDEST.Coroutines
{


	[System.Serializable]
	public class CoroutineCondition : CoroutineSequence
	{

		public CoroutineCondition(CoroutineMethod m_action, Func<bool> m_condition)
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