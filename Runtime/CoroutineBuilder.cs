using System;
using System.Collections.Generic;

namespace THEBADDEST.Coroutines
{


	public class CoroutineBuilder
	{

		readonly List<CoroutineSequence> sequences = new();

		public void Run()
		{
			CoroutineHandler.AfterWait(sequences.ToArray());
		}

		public CoroutineBuilder AfterWait(CoroutineMethod action, float seconds, bool realTime = false)
		{
			sequences.Add(new CoroutineDelay(action, seconds, realTime));
			return this;
		}

		public CoroutineBuilder AfterWait(CoroutineMethod action, Func<bool> condition)
		{
			sequences.Add(new CoroutineCondition(action, condition));
			return this;
		}

		public CoroutineBuilder WaitLoop(CoroutineMethod action, Func<bool> condition, float seconds = 0, bool realTime = false)
		{
			sequences.Add(new CoroutineLoop(action, condition, seconds, realTime));
			return this;
		}

	}


}