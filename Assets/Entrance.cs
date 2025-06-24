using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour, IInteractable
{
    [SerializeField] private string objectName;
    [SerializeField] private GameObject roomPanel;
    private bool playerEntered;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ExitRoom();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (roomPanel && other.GetComponent<PlayerTrigger>())
        {
            UIManager.Instance.GetInfoHandler().ShowInstruction(InstructionType.EnterRoom);
        }
    }

    public void Interact()
    {
        UIManager.Instance.GetInfoHandler().HideTextPopup();
        if (roomPanel)
        {
            EnterRoom();
        }
    }
    public string GetName()
    {
        if (objectName == null)
        {
            return gameObject.name;
        }
        return objectName;
    }

    public void EnterRoom()
    {
        roomPanel.SetActive(true);
        GameService.Instance.playerService.GetPlayerController().Deactivate();
        playerEntered = true;
    }

    public void ExitRoom()
    {
        if (playerEntered)
        {
            roomPanel.SetActive(false);
            GameService.Instance.playerService.GetPlayerController().Activate();
            playerEntered = false;
        }
    }
}
