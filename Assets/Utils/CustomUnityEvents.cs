using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectUnityEvent : UnityEvent<Object> { }
public class FloatUnityEvent : UnityEvent<float> { }
public class IntUnityEvent : UnityEvent<int> { }
public class GameStateUnityEvent : UnityEvent<GameStateController.GameStates> { }
