
using UnityEngine;

public interface ITriggerObject
{
    public abstract void TriggerEnter(GameObject gameObject);
    public abstract void TriggerStay(GameObject gameObject);
    public abstract void TriggerExit(GameObject gameObject);
}
