using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacecraftView : MonoBehaviour
{
    public Rigidbody rb;
    private SpacecraftController controller;

    internal void SetController(SpacecraftController spacecraftController)
    {
        this.controller = spacecraftController;
    }

    private void FixedUpdate()
    {
        if (controller != null)
        {
            controller.Update();
        }
    }
}
