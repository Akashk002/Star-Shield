using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IInteractable
{
    [SerializeField] private string objectName;
    [SerializeField] private GameObject roomPanel;

    public void Interact()
    {

    }
    public string GetName()
    {
        if (objectName == null)
        {
            return gameObject.name;
        }
        return objectName;
    }
}
