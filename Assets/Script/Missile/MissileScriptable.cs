using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MissileScriptableObject", menuName = "ScriptableObjects/MissileScriptableObject")]
public class MissileScriptable : ScriptableObject
{
    public MissileType missileType; // Type of the missile
    public MissileView missileView;
    public float moveSpeed = 10f;
    public float damage = 50f; // Damage value for the missile
    public float boostSpeed = 3f; // For launch phase

    [Header("Sprite")]
    public Sprite spacecraftSprite;
}

