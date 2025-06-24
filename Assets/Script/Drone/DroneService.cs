using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneService
{
    private Dictionary<DroneType, DroneController> droneControllers = new Dictionary<DroneType, DroneController>();
    private List<DroneData> droneDatas = new List<DroneData>();
    public DroneService(List<DroneData> droneDatas)
    {
        this.droneDatas = droneDatas;
        //CreateAllDrone();
    }

    public void CreateAllDrone()
    {
        foreach (DroneData droneData in droneDatas)
        {
            DroneController droneController = new DroneController(droneData.droneScriptable);
            droneController.Configure();
            droneControllers.Add(droneData.droneType, droneController);
        }
    }

    public void CreateDrone(DroneType droneType)
    {
        if (droneControllers.ContainsKey(droneType))
        {
            Debug.LogWarning($"Drone of type {droneType} already exists.");
            return;
        }

        DroneData droneData = droneDatas.Find(data => data.droneType == droneType);

        DroneController droneController = new DroneController(droneData.droneScriptable);
        droneController.Configure();
        droneControllers.Add(droneType, droneController);
    }


    public DroneController GetDroneController(DroneType droneType)
    {
        return droneControllers[droneType];
    }
}
