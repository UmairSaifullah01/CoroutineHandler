using System;
using System.Collections;
using UnityEngine;


namespace THEBADDEST.Coroutines
{


	public class TimeCounter : CoroutineDelay
	{

		private string                resultString;
		private UnityEngine.Coroutine coroutine;
		private bool                  isPlaying = true;
		private int                   incr      = 0;
		private int                   speed     = 1;
		private int                   startFrom = 0;
		private IEnumerator           conditionIterator;

		public TimeCounter(CoroutineMethod OnComplete, float m_seconds, int m_startFrom = 0, bool m_realTime = false) : base(OnComplete, m_seconds)
		{
			startFrom         =  m_startFrom;
			seconds           += m_startFrom;
			conditionIterator =  new WaitUntil(() => isPlaying);
		}


		public void Start(bool isPlay = true)
		{
			coroutine = CoroutineHandler.StartStaticCoroutine(Behaviour());
			isPlaying = isPlay;
		}

		public void Pause()
		{
			isPlaying = false;
		}

		public void Play()
		{
			isPlaying = true;
		}

		public void Plus(float plusSeconds)
		{
			seconds += plusSeconds;
		}

		public void Speed(int speed)
		{
			this.speed = speed;
		}

		public void Stop()
		{
			CoroutineHandler.StopStaticCoroutine(coroutine);
		}

		public string ToString(Format format = Format.minsecs)
		{
			var timeSpan = TimeSpan.FromSeconds(incr + 1);

			switch (format)
			{
				case Format.minsecs:
					return $"{timeSpan.Minutes:00}:{timeSpan.Seconds:00}";
				case Format.milesecesInverse:
					return $"{(int) ((seconds - (incr + 1)) * 1000):000}";
				case Format.milisces:
					return $"{(int) timeSpan.TotalMilliseconds:000}";
				case Format.secs:
					return $"{timeSpan.Seconds:00}";
				default:
					throw new ArgumentOutOfRangeException(nameof(format), format, null);
			}
		}

		public override IEnumerator Behaviour()
		{
			for (incr = startFrom; incr < seconds; incr++)
			{
				yield return conditionIterator;
				if (realTime)
					yield return iterator;
				else yield return iterator2;
			}

			action.Invoke();
		}

		public enum Format
		{

			minsecs,
			milisces,
			milesecesInverse,
			secs

		}

	}


}