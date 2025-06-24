using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpacecraftScriptableObject", menuName = "ScriptableObjects/SpacecraftScriptableObject")]
public class SpacecraftScriptable : ScriptableObject
{
    public SpacecraftType spacecraftType;
    public SpacecraftView spacecraftView;

    [Header("Movement")]
    public float MaxSpeed;
    public float VerticalSpeed;
    public float BrakeSpeed;
    public float AccelerationSpeed;

    [Header("Rotation")]
    public float RotationSpeed;
    public float MouseSensitivity;
    public float MaxPitch;
}

[System.Serializable]
public class SpacecraftData
{
    public SpacecraftType spacecraftType;
    public SpacecraftScriptable spacecraftScriptable;
}

[System.Serializable]
public enum SpacecraftType
{
    Spacecraft_1,
}
