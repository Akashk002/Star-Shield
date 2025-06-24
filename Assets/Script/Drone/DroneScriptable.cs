using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DroneScriptableObject", menuName = "ScriptableObjects/DroneScriptableObject")]
public class DroneScriptable : ScriptableObject
{
    public DroneType droneType;
    public DroneView droneView;

    [Header("Movement")]
    public float Speed;
    public float VerticalSpeed;

    [Header("Rotation")]
    public float RotationSpeed;
    public float MouseSensitivity;
    public float MaxPitch;
}

[System.Serializable]
public class DroneData
{
    public DroneType droneType;
    public DroneScriptable droneScriptable;
}

[System.Serializable]
public enum DroneType
{
    CarrierDrone,
    SecurityDrone,
}