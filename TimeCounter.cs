using System;
using System.Collections;
using UnityEngine;


namespace GameDevUtils.Coroutine
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

		public TimeCounter(Action OnComplete, float m_seconds, int m_startFrom = 0, bool m_realTime = false) : base(OnComplete, m_seconds)
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

		public void Speed(int m_speed)
		{
			speed = m_speed;
		}

		public void Stop()
		{
			CoroutineHandler.StopStaticCoroutine(coroutine);
		}

		public string Output(Format format = Format.minsecs)
		{
			if (format == Format.minsecs)
			{
				int mints = (incr + 1) / 60;
				int sec   = (incr + 1) % 60;
				resultString = (mints).ToString("00") + " : " + (sec).ToString("00");
			}
			else if (format == Format.milesecesInverse)
			{
				int mints = (int) ((seconds - (incr + 1)) / 60);
				int sec   = (int) ((seconds - (incr + 1)) % 60);
				resultString = (mints).ToString("00") + " : " + (sec).ToString("00");
			}
			else if (format == Format.milisces)
			{
				resultString = (incr / 1000).ToString("000");
			}
			else
			{
				resultString = incr.ToString("00");
			}

			return resultString;
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