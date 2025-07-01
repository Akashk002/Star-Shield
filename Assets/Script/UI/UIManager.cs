using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : GenericMonoSingleton<UIManager>
{
    [Header("UI Panels")]
    public PlayerUIManager playerPanel;
    public SpaceCraftUIManager spacecraftPanel;
    public DroneUIManager droneUIManager;
    public InfoHandler infoHandler;
    public MinimapIconFollow minimapIconPanel;

    [Header("Room Panels")]
    public GameObject HomePanel;
    public GameObject DroneControlPanel;
    public GameObject SpaceCraftSelectionPanel;

    public void ShowPanel(PanelType panelType)
    {
        playerPanel.gameObject.SetActive(false);
        spacecraftPanel.gameObject.SetActive(false);
        droneUIManager.gameObject.SetActive(false);

        switch (panelType)
        {
            case PanelType.Player:
                playerPanel.gameObject.SetActive(true);
                break;
            case PanelType.Spacecraft:
                spacecraftPanel.gameObject.SetActive(true);
                break;
            case PanelType.Drone:
                droneUIManager.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    public InfoHandler GetInfoHandler()
    {
        return infoHandler;
    }
}

public enum PanelType
{
    Player,
    Spacecraft,
    Drone,
}