using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpaceCraftScriptableObject", menuName = "ScriptableObjects/EnemySpaceCraftScriptableObject")]
public class EnemySpaceCraftScriptable : ScriptableObject
{
    public EnemySpaceCraftView enemySpaceCraftView;
    public MissileType missileType; // Type of the missile used by the enemy spacecraft
    public float health = 100f; // Health of the enemy spacecraft
    public float moveSpeed = 10f;
    public float fireRate = 1f;
}
