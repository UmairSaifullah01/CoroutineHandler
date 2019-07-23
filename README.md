# CoroutineHandler
All useful functions to perform Coroutine in unity

<------------Usage-------------->

1. Static Corrotines

Disc:

Create coroutines and call from any script other then MonoBehaviour scripts.
ie. Call coroutines from  non monobehaviour scripts

           CoroutineHandler.Instance.Start_Coroutine(AnyCoroutineFuction);


Flow Beahviour Usage :

           FlowBehaviour.Builder (this).AfterWait (any Action , wait).AfterWait (any other Action, wait).Start ());


