using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DroneScriptableObject", menuName = "ScriptableObjects/DroneScriptableObject")]
public class DroneScriptable : ScriptableObject
{
    [Header("Movement")]
    public float Speed;
    public float VerticalSpeed;

    [Header("Rotation")]
    public float RotationSpeed;
    public float MouseSensitivity;
    public float MaxPitch;
}

