using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UMGS
{
    /// <summary>
    /// This class allows us to start Coroutines from non-Monobehaviour scripts.
    /// it have totally all functins for corotine actions like a little wait then use AfterWait() function
    /// </summary>
    #region Corotine Handler Main Class
    public class CoroutineHandler : Singleton<CoroutineHandler>
    {

        #region Simple Corotine functions.......................
        protected override void Awake ()
        {
            base.Awake ();
            updator = new UMUpdate (this);
        }
        static public Coroutine StartStaticCoroutine (IEnumerator coroutine)
        {
            return Instance.StartCoroutine (coroutine);
        }
        static public void StopStaticCoroutine (Coroutine coroutine)
        {
            Instance.StopCoroutine (coroutine);
        }
        public Coroutine Start_Coroutine (IEnumerator coroutine)
        {
            return Instance.StartCoroutine (coroutine);
        }

        public static void StopAll ()
        {
            Instance.StopAllCoroutines ();
        }
        #endregion
        #region Update With Coroutine
        private static UMUpdate updator;
        public static void DoUpdate (Action action, float delay = 0)
        {
            updator.DoUpdate (action, delay);
        }

        public static void RemoveUpdate (Action action, float delay = 0)
        {
            updator.RemoveUpdate (action, delay);
        }
        public static void StopUpdate (float delay = 0)
        {
            updator.StopUpdate (delay);
        }
        #endregion
        #region AfterWait for single delay
        public static Coroutine AfterWait (Action action, float seconds, bool realTime = false)
        {
            return Instance.StartCoroutine (AfterWaitCoroutine (action, seconds, realTime));
        }
        public static Coroutine AfterWait (MonoBehaviour Obj, Action action, float seconds, bool realTime = false)
        {
            return Obj.StartCoroutine (AfterWaitCoroutine (action, seconds, realTime));
        }
        static IEnumerator AfterWaitCoroutine (Action action, float seconds, bool realTime)
        {
            yield return new CoroutineDelay (action, seconds, realTime).Behaviour ();
        }
        #endregion
        #region AfterWait for multipule functions
        public static Coroutine AfterWait (bool realTime = false, params CoroutineSequence[] sequences)
        {
            return Instance.StartCoroutine (AfterWaitCoroutine (realTime, sequences));
        }
        public static Coroutine AfterWait (MonoBehaviour Obj, bool realTime = false, params CoroutineSequence[] sequences)
        {
            return Obj.StartCoroutine (AfterWaitCoroutine (realTime, sequences));
        }
        private static IEnumerator AfterWaitCoroutine (bool realTime, CoroutineSequence[] sequences)
        {
            foreach (var item in sequences)
            {
                yield return item.Behaviour ();
            }

        }
        #endregion
        #region AfterWait for with condition
        public static Coroutine AfterWait (Action action, Func<bool> condition)
        {
            return Instance.StartCoroutine (AfterWaitCoroutine (action, condition));
        }
        public static Coroutine AfterWait (MonoBehaviour Obj, Action action, Func<bool> condition)
        {
            return Obj.StartCoroutine (AfterWaitCoroutine (action, condition));
        }
        static IEnumerator AfterWaitCoroutine (Action action, Func<bool> condition)
        {
            yield return new CoroutineCondition (action, condition).Behaviour ();
        }
        #endregion
        #region simpleWaitLoop
        public static Coroutine WaitLoop (Action action, Func<bool> condition, float seconds = 0, bool realTime = false)
        {
            return Instance.StartCoroutine (WaitLoopCoroutine (action, condition, seconds, realTime));
        }
        public static Coroutine WaitLoop (MonoBehaviour Obj, Action action, Func<bool> condition, float seconds = 0, bool realTime = false)
        {
            return Obj.StartCoroutine (WaitLoopCoroutine (action, condition, seconds, realTime));
        }
        static IEnumerator WaitLoopCoroutine (Action action, Func<bool> condition, float seconds, bool realTime)
        {
            CoroutineDelay delay = new CoroutineDelay (action, seconds, realTime);
            while (!condition ())
            {
                yield return new WaitForFixedUpdate ();
                if (seconds == 0)
                    action.Invoke ();
                else
                    yield return delay.Behaviour ();
            }

        }
        #endregion

    }
    #endregion
    #region Extension Methods
    public static class CoroutineStatic
    {
        public static Coroutine AfterWait (this MonoBehaviour Obj, Action action, float seconds, bool realTime = false)
        {
            return CoroutineHandler.AfterWait (Obj, action, seconds, realTime);
        }
        public static Coroutine AfterWait (this MonoBehaviour Obj, bool realTime = false, params CoroutineSequence[] sequences)
        {
            return CoroutineHandler.AfterWait (Obj, realTime, sequences);
        }
        public static Coroutine AfterWait (this MonoBehaviour Obj, Action action, Func<bool> condition)
        {
            return CoroutineHandler.AfterWait (Obj, action, condition);
        }
    }
    #endregion
    #region Classes for use
    [System.Serializable]
    public abstract class CoroutineSequence
    {
        public Action action;
        public abstract IEnumerator Behaviour ();
    }
    [System.Serializable]
    public class CoroutineDelay : CoroutineSequence
    {
        public float seconds;
        public bool realTime;
        public CoroutineDelay (Action m_action, float m_seconds, bool m_realTime = false)
        {
            action = m_action;
            seconds = m_seconds;
            realTime = m_realTime;
        }

        public override IEnumerator Behaviour ()
        {
            if (realTime)
                yield return new WaitForSecondsRealtime (seconds);
            else
                yield return new WaitForSeconds (seconds);
            action.Invoke ();
        }
    }
    [System.Serializable]
    public class CoroutineCondition : CoroutineSequence
    {
        public Func<bool> condition;
        public CoroutineCondition (Action m_action, Func<bool> m_condition)
        {
            action = m_action;
            condition = m_condition;
        }

        public override IEnumerator Behaviour ()
        {
            yield return new WaitUntil (condition);
            action.Invoke ();
        }
    }




    public class UMUpdate
    {
        public Dictionary<float, UpdateData> updateCoroutines = new Dictionary<float, UpdateData> ();
        MonoBehaviour mono;
        public UMUpdate (MonoBehaviour mono)
        {
            this.mono = mono;
        }
        public void DoUpdate (Action action, float delay)
        {
            if (updateCoroutines.TryGetValue (delay, out UpdateData result))
            {
                result.action += action;
                if (result.coroutine == null)
                    result.coroutine = mono.StartCoroutine (DoUpdateCoroutine (result.action, delay));
            }
            else
            {
                UpdateData data = new UpdateData () { action = action, coroutine = mono.StartCoroutine (DoUpdateCoroutine (action, delay)) };
                updateCoroutines.Add (delay, data);

            }



        }
        public void RemoveUpdate (Action action, float delayRegistor)
        {
            if (updateCoroutines.TryGetValue (delayRegistor, out UpdateData result))
            {
                result.action -= action;
                if (result.action == null)
                {
                    mono.StopCoroutine (result.coroutine);
                    updateCoroutines.Remove (delayRegistor);
                }
            }
        }
        public void StopUpdate (float delayRegistor)
        {
            if (updateCoroutines.TryGetValue (delayRegistor, out UpdateData result))
            {
                mono.StopCoroutine (result.coroutine);
                updateCoroutines.Remove (delayRegistor);
            }
        }
        IEnumerator DoUpdateCoroutine (Action action, float delay)
        {
            while (true)
            {
                yield return new WaitForFixedUpdate ();
                yield return new WaitForSeconds (delay);
                action.Invoke ();
            }
        }

        public struct UpdateData
        {
            public Action action;
            public Coroutine coroutine;
        }
    }
    #endregion

    #region Timer
    public class TimeCounter : CoroutineDelay
    {
        private string resultString;
        private Coroutine coroutine;
        private bool isPlaying = true;
        private int incr = 0;
        private int speed = 1;
        private int startFrom = 0;
        public TimeCounter (Action OnComplete, float m_seconds, int m_startFrom = 0, bool m_realTime = false) : base (OnComplete, m_seconds, m_realTime)
        {
            action = OnComplete;
            seconds = m_seconds;
            realTime = m_realTime;
            startFrom = m_startFrom;
            seconds += m_startFrom;
        }
        public void Start (bool isPlay = true)
        {
            coroutine = CoroutineHandler.StartStaticCoroutine (Behaviour ());
            isPlaying = isPlay;
        }
        public void Pause ()
        {
            isPlaying = false;
        }
        public void Play ()
        {
            isPlaying = true;
        }
        public void Plus (float plusSeconds)
        {
            seconds += plusSeconds;
        }
        public void Speed (int m_speed)
        {
            speed = m_speed;
        }
        public void Stop ()
        {
            CoroutineHandler.StopStaticCoroutine (coroutine);
        }
        public string Output (Format format = Format.minsecs)
        {
            if (format == Format.minsecs)
            {
                int mints = ( incr + 1 ) / 60;
                int sec = ( incr + 1 ) % 60;
                resultString = ( mints ).ToString ("00") + " : " + ( sec ).ToString ("00");
            }
            else if (format == Format.milesecesInverse)
            {
                int mints = (int) ( ( seconds - ( incr + 1 ) ) / 60 );
                int sec = (int) ( ( seconds - ( incr + 1 ) ) % 60 );
                resultString = ( mints ).ToString ("00") + " : " + ( sec ).ToString ("00");
            }
            else if (format == Format.milisces)
            {
                resultString = ( incr / 1000 ).ToString ("000");
            }
            else
            {
                resultString = incr.ToString ("00");
            }
            return resultString;
        }
        public override IEnumerator Behaviour ()
        {

            for (incr = startFrom; incr < seconds; incr++)
            {

                yield return new WaitUntil (() => isPlaying);

                if (realTime)
                    yield return new WaitForSecondsRealtime (1 / speed);
                else
                    yield return new WaitForSeconds (1 / speed);

            }


            action.Invoke ();
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
#endregion