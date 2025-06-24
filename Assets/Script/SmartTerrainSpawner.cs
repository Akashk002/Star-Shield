using System.Collections.Generic;
using UnityEngine;

public class SmartTerrainSpawner : MonoBehaviour
{
    public Terrain terrain;
    public List<Rock> objectSToSpawn = new List<Rock>();
    public int spawnCount = 10;
    public float maxSlopeAngle = 30f; // Maximum acceptable slope in degrees

    void Start()
    {
        Transform parent = new GameObject("Stones").transform;

        for (int i = 0; i < spawnCount / objectSToSpawn.Count; i++)
        {
            for (int j = 0; j < objectSToSpawn.Count; j++)
            {
                Rock rock = objectSToSpawn[j];
                TrySpawn(rock, parent);
            }
        }
    }

    void TrySpawn(Rock rock, Transform parent)
    {
        TerrainData tData = terrain.terrainData;
        Vector3 terrainPos = terrain.transform.position;

        for (int attempts = 0; attempts < 30; attempts++)  // Limit to avoid infinite loop
        {
            float terrainWidth = tData.size.x;
            float terrainLength = tData.size.z;

            float randomX = Random.Range(0f, terrainWidth);
            float randomZ = Random.Range(0f, terrainLength);

            // Normalize to get steepness
            float xNormalized = randomX / terrainWidth;
            float zNormalized = randomZ / terrainLength;
            float slope = tData.GetSteepness(xNormalized, zNormalized);

            if (slope > maxSlopeAngle) continue;  // Retry if too steep

            // Get the height (Y position)
            float y = terrain.SampleHeight(new Vector3(randomX, 0f, randomZ));
            Vector3 worldPos = new Vector3(randomX, y, randomZ) + terrainPos;

            GameObject stonePrefab = Instantiate(rock.gameObject, worldPos, Quaternion.identity);
            stonePrefab.transform.SetParent(parent);

            return;
        }

        Debug.LogWarning("Failed to find suitable spawn point.");
    }
}
