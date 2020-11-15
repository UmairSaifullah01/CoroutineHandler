using System;
using System.Collections;


namespace GameDevUtils.Coroutine
{


	[System.Serializable]
	public abstract class CoroutineSequence
	{

		public    Action      action;
		protected IEnumerator iterator;


		public abstract IEnumerator Behaviour();

	}


}