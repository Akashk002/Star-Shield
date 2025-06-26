using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IInteractable
{
    [SerializeField] private BuildingType buildingType;
    [SerializeField] private GameObject roomPanel;

    public void Interact()
    {

    }
    public string GetName()
    {
        return SplitCamelCase(buildingType.ToString());
    }

    public static string SplitCamelCase(string input)
    {
        return System.Text.RegularExpressions.Regex.Replace(
            input,
            "(\\B[A-Z])",
            " $1"
        );
    }
}

public enum BuildingType
{
    Home,
    MainOffice,
    ReserchCenter,
    RunwayStrip,
    DroneControlRoom,
    DroneChargingStation,
    Reactor,
    Farm,
    MachineBuildingPlant,
    ResourceWareHouse,
    SignalCommandCenter,
    GeothermalGenerator,
    SolarGenerator,
    SolarPanel
}