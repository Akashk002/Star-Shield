using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] List<Building> buildings = new List<Building>();

    private int buildingsDamage = 0;

    public void AddDamage(int val)
    {
        buildingsDamage += val;

        Debug.Log($"Building damage added: {val}. Total damage: {buildingsDamage}");

        if (buildingsDamage >= 1000)
        {
            Debug.LogWarning("Buildings are damaged beyond repair!");
            buildingsDamage = 0; // Reset damage count after warning
        }
    }

    public Vector3 GetRandomBuildingPos()
    {
        int randomIndex = Random.Range(0, buildings.Count);
        return buildings[randomIndex].transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
