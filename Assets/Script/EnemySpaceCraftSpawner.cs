using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpaceCraftSpawner : GenericMonoSingleton<EnemySpaceCraftSpawner>
{
    [SerializeField] private List<SpawnPointData> spawnPoints = new List<SpawnPointData>();

    public SpawnPointData GetspawnPoint()
    {
        int randomIndex = Random.Range(0, spawnPoints.Count);
        UIManager.Instance.minimapIconPanel.StartBlink(randomIndex);
        return spawnPoints[randomIndex];
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SpawnPointData spawnPoint = GetspawnPoint();
            GameService.Instance.enemySpaceCraftService.CreateEnemySpaceCraft(spawnPoint.initialPosition.position, spawnPoint.targetPosition.position);

        }
    }
}

[System.Serializable]
public class SpawnPointData
{
    public Transform initialPosition;
    public Transform targetPosition;
}