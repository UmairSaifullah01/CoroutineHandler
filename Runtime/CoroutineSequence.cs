using System.Collections;


namespace THEBADDEST.Coroutines
{


	public delegate void CoroutineMethod();
	[System.Serializable]
	public abstract class CoroutineSequence
	{

		public    CoroutineMethod action;
		protected IEnumerator     iterator;


		public abstract IEnumerator Behaviour();

	}


}