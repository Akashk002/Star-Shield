using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DroneUIManager : MonoBehaviour
{
    [SerializeField] private List<RockUIData> rockUIDatas = new List<RockUIData>();
    [SerializeField] private GridLayoutGroup stoneInfoPanel;
    [SerializeField] private Button switchDroneButton;
    [SerializeField] private TextMeshProUGUI switchDroneButtonText;
    [SerializeField] private Button ToggleSurveillanceButton;
    [SerializeField] private TextMeshProUGUI ToggleSurveillanceButtonText;
    [SerializeField] private Slider batterySlider;
    private DroneScriptable currentDroneScriptable;

    private void Start()
    {
        switchDroneButton.onClick.AddListener(SwitchDrone);
        ToggleSurveillanceButton.onClick.AddListener(ToggleSurveillanceMode);
    }

    private void FixedUpdate()
    {
        UpdateDroneBattery();
    }

    public void SetRockCount(RockType rockType, int Val)
    {
        RockUIData rockUIData = rockUIDatas.Find(data => data.rockType == rockType);
        rockUIData.SetText(Val);
    }

    private void SwitchDrone()
    {
        GameService.Instance.droneService.SwitchDrone();
        currentDroneScriptable = GameService.Instance.droneService.currentDroneController.GetDroneScriptable();
        stoneInfoPanel.gameObject.SetActive(currentDroneScriptable.droneType == DroneType.CarrierDrone);
        ToggleSurveillanceButton.gameObject.SetActive(currentDroneScriptable.droneType == DroneType.SecurityDrone);
        UpdateSwitchDroneButtonText();
    }

    public void ToggleSurveillanceMode()
    {
        GameService.Instance.droneService.GetDroneController().ToggleDroneSurveillanceState();
        UpdateToggleSurveillanceButtonText();
    }

    private void UpdateSwitchDroneButtonText()
    {
        switchDroneButtonText.SetText((currentDroneScriptable.droneType != DroneType.CarrierDrone) ? "Switch to Carrier Drone" : "Switch to Security Drone");
    }
    private void UpdateToggleSurveillanceButtonText()
    {
        DroneState droneState = GameService.Instance.droneService.GetDroneController().GetDroneState();
        ToggleSurveillanceButtonText.SetText((droneState != DroneState.Surveillance) ? "Turn On Surveillance Mode" : "Turn Off Surveillance Mode");
    }

    public void UpdateDroneBattery()
    {
        if (currentDroneScriptable)
        {
            batterySlider.value = currentDroneScriptable.droneBattery;
        }
    }
}
