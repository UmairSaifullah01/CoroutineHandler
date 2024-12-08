# Coroutine Handler Documentation

This documentation describes the functionality of the `CoroutineHandler` class and its related components in the `THEBADDEST.Coroutines` namespace. The class provides utility methods for managing coroutines in Unity outside of MonoBehaviour.

## Overview
The `CoroutineHandler` class simplifies coroutine management by creating a dedicated `MonoRunner` GameObject to run coroutines independently of other MonoBehaviours. This allows developers to manage coroutines without relying on specific GameObjects or MonoBehaviours in the scene.

---

## MonoRunner

The `MonoRunner` component is the backbone for managing coroutines. It is automatically instantiated when the application starts through the `CreateCoroutineHandler` method, which uses the `RuntimeInitializeOnLoadMethod` attribute.

```csharp
[RuntimeInitializeOnLoadMethod]
public static void CreateCoroutineHandler()
```

---

## Public Methods

### Simple Coroutine Functions

#### StartStaticCoroutine
Starts a coroutine on the `MonoRunner` instance.
```csharp
public static UnityEngine.Coroutine StartStaticCoroutine(IEnumerator coroutine)
```
**Parameters:**
- `coroutine`: The coroutine to start.

**Returns:**
- `UnityEngine.Coroutine`: The coroutine that was started.

#### StopStaticCoroutine
Stops the specified coroutine.
```csharp
public static void StopStaticCoroutine(UnityEngine.Coroutine coroutine)
```
**Parameters:**
- `coroutine`: The coroutine to stop.

#### StopAll
Stops all coroutines running on the `MonoRunner` instance.
```csharp
public static void StopAll()
```

---

### Update with Coroutine

#### DoUpdate
Executes a method repeatedly with an optional delay.
```csharp
public static void DoUpdate(CoroutineMethod action, float delay = 0)
```
**Parameters:**
- `action`: The method to invoke.
- `delay`: Optional delay between executions.

#### RemoveUpdate
Removes a previously registered update action.
```csharp
public static void RemoveUpdate(CoroutineMethod action, float delay = 0)
```

#### StopUpdate
Stops all update actions.
```csharp
public static void StopUpdate(float delay = 0)
```

---

### AfterWait (Single Delay)

#### AfterWait
Starts a coroutine to execute an action after a specified delay.
```csharp
public static UnityEngine.Coroutine AfterWait(CoroutineMethod action, float seconds, bool realTime = false)
```
**Parameters:**
- `action`: The action to call after the delay.
- `seconds`: Time to wait.
- `realTime`: If `true`, uses real-time; otherwise, uses game-time.

#### AfterWait (Overload)
Runs the same functionality but accepts a MonoBehaviour.
```csharp
public static UnityEngine.Coroutine AfterWait(MonoBehaviour mono, CoroutineMethod action, float seconds, bool realTime = false)
```

---

### AfterWait (Multiple Actions)

#### AfterWait (Multiple Sequences)
Executes multiple coroutine sequences sequentially.
```csharp
public static UnityEngine.Coroutine AfterWait(params CoroutineSequence[] sequences)
```
**Parameters:**
- `sequences`: Array of coroutine sequences.

---

### AfterWait with Condition

#### AfterWait (Condition)
Starts a coroutine that waits for a condition to be true before executing an action.
```csharp
public static UnityEngine.Coroutine AfterWait(CoroutineMethod action, Func<bool> condition)
```
**Parameters:**
- `action`: The action to invoke.
- `condition`: The condition to satisfy.

---

### WaitLoop

#### WaitLoop
Executes an action repeatedly while a condition is false.
```csharp
public static UnityEngine.Coroutine WaitLoop(CoroutineMethod action, Func<bool> condition, float seconds = 0, bool realTime = false)
```
**Parameters:**
- `action`: The action to invoke.
- `condition`: The condition to evaluate.
- `seconds`: Optional delay between actions.
- `realTime`: Whether to use real-time or game-time.

---

## Auxiliary Classes

### CoroutineUpdate
Manages repeating coroutine actions.

### CoroutineDelay
Handles delayed coroutine execution.

### CoroutineCondition
Handles condition-based coroutine execution.

### CoroutineSequence
Encapsulates multiple coroutines to execute sequentially.

---

## Examples

### Start a Coroutine
```csharp
CoroutineHandler.StartStaticCoroutine(MyCoroutine());

IEnumerator MyCoroutine()
{
    yield return new WaitForSeconds(1);
    Debug.Log("Coroutine executed!");
}
```

### Execute an Action After a Delay
```csharp
CoroutineHandler.AfterWait(() => Debug.Log("Delayed action!"), 2);
```

### Wait for a Condition
```csharp
CoroutineHandler.AfterWait(() => Debug.Log("Condition met!"), () => Input.GetKeyDown(KeyCode.Space));
```

### Loop While a Condition is False
```csharp
CoroutineHandler.WaitLoop(() => Debug.Log("Waiting..."), () => Time.time > 10, 1);
```

---

## Conclusion
The `CoroutineHandler` class provides a flexible and centralized approach for managing coroutines in Unity. By using `MonoRunner` and utility methods, you can simplify coroutine usage while keeping code decoupled and maintainable.


## 🚀 About Me
Umair Saifullah ~ a unity developer from Pakistan.


## License

[MIT](https://choosealicense.com/licenses/mit/)

