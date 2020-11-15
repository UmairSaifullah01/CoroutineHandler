# CoroutineHandler
All useful functions to perform Coroutines in unity

Dependencies 
        https://github.com/UmairSaifullah01/GameDevUtils/blob/master/Singleton.cs

<------------Usage-------------->

1. Static Coroutines

Description:

Create coroutines and call from any script other then MonoBehaviour scripts.
ie. Call coroutines from  non MonoBehaviour scripts

       CoroutineHandler.StartStaticCoroutine(AnyCoroutineFuction);


Coroutine Builder Usage :

       CoroutineBuilder.Builder (this).AfterWait (any Action , wait).AfterWait (any other Action, wait).Run ();


