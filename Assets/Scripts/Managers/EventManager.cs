using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    Dictionary<string, Action<object[]>> events = new Dictionary<string, Action<object[]>>();
    public void Subscribe(string eventType, Action<object[]> action)
    {
        if (events.ContainsKey(eventType))
            events[eventType] += action;
        else
        {
            events.Add(eventType, action);
        }
    }
    public void Unsubscribe(string eventType, Action<object[]> action)
    {
        if (events.ContainsKey(eventType))
            events[eventType] -= action;
    }

    public void SendEvent(string eventType,object[] _params = null)
    {
        if (events.ContainsKey(eventType))
        {
            events[eventType]?.Invoke(_params);
        }
    }


}
