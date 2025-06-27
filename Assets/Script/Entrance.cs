using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour, IInteractable
{
    [SerializeField] private EntranceType entranceType;
    [SerializeField] private GameObject roomPanel;
    private bool playerEntered;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ExitRoom();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        TriggerGameObject triggerGameObject;
        if (roomPanel && other.TryGetComponent(out triggerGameObject) && triggerGameObject.triggerByEntrance)
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
        return SplitCamelCase(entranceType.ToString());
    }

    public static string SplitCamelCase(string input)
    {
        return System.Text.RegularExpressions.Regex.Replace(
            input,
            "(\\B[A-Z])",
            " $1"
        );
    }

    public void EnterRoom()
    {
        roomPanel.SetActive(true);
        GameService.Instance.playerService.GetPlayerController().Deactivate();
        playerEntered = true;

        if (entranceType == EntranceType.DroneControlRoomEntrance)
        {
            UIManager.Instance.ShowPanel(PanelType.Drone);
            GameService.Instance.droneService.StartDrone(DroneType.CarrierDrone);
        }
    }

    public void ExitRoom()
    {
        if (playerEntered)
        {
            roomPanel.SetActive(false);
            GameService.Instance.playerService.GetPlayerController().Activate();
            playerEntered = false;

            if (entranceType == EntranceType.DroneControlRoomEntrance)
            {
                GameService.Instance.droneService.StopDrone();
            }
        }
    }
}

public enum EntranceType
{
    HomeEntrance,
    DroneControlRoomEntrance,
}
