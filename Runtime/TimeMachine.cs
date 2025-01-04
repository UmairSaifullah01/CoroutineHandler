using UnityEngine;


namespace THEBADDEST.Coroutines
{


	public delegate void EverySecondActionDelegate();

	[CreateAssetMenu(menuName = "THEBADDEST/Coroutines/TimeMachine", fileName = "TimeMachine", order = 0)]
	public class TimeMachine : ScriptableObject
	{

		private int                                    gameTime = 0;
		public int                             GameTime => gameTime;
		public event EverySecondActionDelegate OnEverySecond;

		public void Initialize()
		{
			gameTime=0;
			CoroutineHandler.DoUpdate(Tick, 1);
		}

		void Tick()
		{
			gameTime++;
			OnEverySecond?.Invoke();
		}

		public void Stop()
		{
			CoroutineHandler.RemoveUpdate(Tick,1);
		}

	}


}