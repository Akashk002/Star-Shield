using System;
using UnityEngine;
public class EventService
{
    private static EventService instance;
    public static EventService Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EventService();
            }
            return instance;
        }
    }

    public EventController OnRideSpaceCraft { get; private set; }
    public EventController<string> OnCollideObject { get; private set; }
    public EventController<InstructionType> OnCollideRock { get; private set; }
    //public EventController<DefenderType, Vector3, bool> OnPlaceDefender { get; private set; }
    //public EventController<AttackerType, Slot, AttackerController> OnSpawnAttacker { get; private set; }


    public EventService()
    {
        OnRideSpaceCraft = new EventController();
        OnCollideObject = new EventController<string>();
        OnCollideRock = new EventController<InstructionType>();
        //OnPlaceDefender = new EventController<DefenderType, Vector3, bool>();
        //OnSpawnAttacker = new EventController<AttackerType, Slot, AttackerController>();
    }
}
public class EventController<T, K, R>
{
    private Func<T, K, R> baseEvent;

    public R InvokeEvent(T type, K type2) => baseEvent != null ? baseEvent(type, type2) : default;

    public void AddListener(Func<T, K, R> listener) => baseEvent += listener;

    public void RemoveListener(Func<T, K, R> listener) => baseEvent -= listener;
}

public class EventController<T, K>
{
    public event Action<T, K> baseEvent;
    public void InvokeEvent(T type, K type2) => baseEvent?.Invoke(type, type2);
    public void AddListener(Action<T, K> listener) => baseEvent += listener;
    public void RemoveListener(Action<T, K> listener) => baseEvent -= listener;
}

public class EventController<T>
{
    public event Action<T> baseEvent;
    public void InvokeEvent(T type) => baseEvent?.Invoke(type);
    public void AddListener(Action<T> listener) => baseEvent += listener;
    public void RemoveListener(Action<T> listener) => baseEvent -= listener;
}
public class EventController
{
    public event Action baseEvent;
    public void InvokeEvent() => baseEvent?.Invoke();
    public void AddListener(Action listener) => baseEvent += listener;
    public void RemoveListener(Action listener) => baseEvent -= listener;
}


