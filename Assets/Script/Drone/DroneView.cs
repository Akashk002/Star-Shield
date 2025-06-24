using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneView : MonoBehaviour
{
    public Rigidbody rb;
    public Camera cam;
    private DroneController controller;

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

