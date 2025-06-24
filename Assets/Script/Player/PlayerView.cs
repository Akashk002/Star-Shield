using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public CinemachineFreeLook cam;
    public CharacterController controller;
    public Animator animator;
    public GameObject bagPack;
    private PlayerController playerController;

    public void SetController(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (playerController != null)
        {
            playerController.Update();
            playerController.Interact();
        }
    }

    public void OnPlayerTriggerEnter(GameObject gameObject)
    {
        IInteractable interactable;
        if (gameObject.TryGetComponent(out interactable))
        {
            UIManager.Instance.GetInfoHandler().ShowObjectName(interactable.GetName());

            if (gameObject.GetComponent<Rock>())
            {
                UIManager.Instance.GetInfoHandler().ShowInstruction(InstructionType.RockCollect);
            }
        }
    }

    internal void OnPlayerTriggerStay(GameObject gameObject)
    {
        IInteractable interactable;
        if (gameObject.TryGetComponent(out interactable) && playerController.IsInteracted)
        {
            playerController.IsInteracted = false;

            Debug.Log($"Interacting with {gameObject.name}");

            if (gameObject.GetComponent<Entrance>() || (gameObject.GetComponent<Rock>() && IsCarryBagPack()))
            {
                interactable.Interact();
            }
            else
            {
                UIManager.Instance.GetInfoHandler().ShowInstruction(InstructionType.CarryBagpack);
            }
        }
    }

    public void OnPlayerTriggerExit(GameObject gameObject)
    {
        if (gameObject.GetComponent<IInteractable>() != null)
        {
            UIManager.Instance.GetInfoHandler().HideTextPopup();
        }
    }
    public bool IsCarryBagPack()
    {
        return bagPack.activeSelf;
    }

    public void CarryBagPack()
    {
        bagPack.SetActive(!IsCarryBagPack());
    }
}
