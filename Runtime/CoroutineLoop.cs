using System;
using System.Collections;


namespace THEBADDEST.Coroutines
{


	public class CoroutineLoop : CoroutineSequence
	{

		Func<bool>              condition;
		readonly CoroutineDelay delay;
		float                   seconds;
		public CoroutineLoop(CoroutineMethod action, Func<bool> condition, float seconds, bool realTime)
		{
			this.action    = action;
			this.seconds   = seconds;
			this.condition = condition;
			this.delay     = new CoroutineDelay(base.action, seconds, realTime);
		}

		IEnumerator PerFrameAction()
		{
			this.action?.Invoke();
			yield return null;
		}

		public override IEnumerator Behaviour()
		{
			
			while (!condition())
			{
				this.iterator = seconds <= 0 ? PerFrameAction() : delay.Behaviour();
				yield return iterator;
			}
		}

	}


}