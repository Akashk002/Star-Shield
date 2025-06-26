using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneView : MonoBehaviour, ITriggerObject
{
    public Rigidbody rb;
    public Camera cam;
    private DroneController controller;

    public void TriggerEnter(GameObject gameObject)
    {
        if (gameObject.GetComponent<Rock>())
        {
            UIManager.Instance.GetInfoHandler().ShowInstruction(InstructionType.RockCollect);
        }
    }

    public void TriggerStay(GameObject gameObject)
    {
        IInteractable interactable;
        if (gameObject.TryGetComponent(out interactable))
        {
            if ((gameObject.GetComponent<Rock>() && controller.IsInteracted))
            {
                controller.IsInteracted = false;
                interactable.Interact();
                Rock rock = gameObject.GetComponent<Rock>();
                controller.AddRock(rock.GetRockType());
            }
        }
    }

    public void TriggerExit(GameObject gameObject)
    {
        if (gameObject.GetComponent<IInteractable>() != null)
        {
            if (gameObject.GetComponent<Rock>())
            {
                UIManager.Instance.GetInfoHandler().HideTextPopup();
            }
        }
    }

    internal void SetController(DroneController controller)
    {
        this.controller = controller;
    }

    private void Update()
    {
        if (controller != null)
        {
            controller.Update();
        }
    }
}

