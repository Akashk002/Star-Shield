using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpaceCraftScriptableObject", menuName = "ScriptableObjects/EnemySpaceCraftScriptableObject")]
public class EnemySpaceCraftScriptable : ScriptableObject
{
    public float moveSpeed = 10f;
    public float fireRate = 1f;
}
