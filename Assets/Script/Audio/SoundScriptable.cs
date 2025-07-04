using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundScriptableObject", menuName = "ScriptableObjects/SoundScriptableObject")]
public class SoundScriptable : ScriptableObject
{
    public AudioClip clip;
    public SoundType soundType;
    public bool loop;
    public float volume = 1f;
    public float spatialBlend = 1f; // 1 = 3D sound
}



