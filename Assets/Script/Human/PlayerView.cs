using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    public CharacterController controller;
    public Animator animator;
    private PlayerController playerController;

    public void SetController(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerController.Update();
    }
}
