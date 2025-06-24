using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacecraftView : MonoBehaviour
{
    public Rigidbody rb;
    public CinemachineVirtualCamera cam;
    private SpacecraftController controller;

    internal void SetController(SpacecraftController spacecraftController)
    {
        this.controller = spacecraftController;
    }

    private void Update()
    {
        if (controller != null)
        {
            controller.Update();
        }
    }
}
