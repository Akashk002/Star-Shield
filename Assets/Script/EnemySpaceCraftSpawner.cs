using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpaceCraftSpawner : GenericMonoSingleton<EnemySpaceCraftSpawner>
{
    [SerializeField] private List<SpawnPointData> spawnPoints = new List<SpawnPointData>();
    [SerializeField] private List<Image> pointers = new List<Image>();
    [SerializeField] private float blinkInterval = 0.5f; // toggle every 0.5s
    private Image targetImage;
    private LTDescr blinkTween;
    private float totalDuration = 3f;

    public SpawnPointData GetspawnPoint()
    {
        int randomIndex = Random.Range(0, spawnPoints.Count);
        targetImage = pointers[randomIndex];
        return spawnPoints[randomIndex];
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SpawnPointData spawnPoint = GetspawnPoint();
            GameService.Instance.enemySpaceCraftService.CreateEnemySpaceCraft(spawnPoint.initialPosition.position, spawnPoint.targetPosition.position);

            BlinkImageForDuration(totalDuration);
        }
    }

    public void BlinkImageForDuration(float duration)
    {
        float elapsed = 0f;

        void ToggleLoop()
        {
            if (targetImage != null)
                targetImage.enabled = !targetImage.enabled;

            elapsed += blinkInterval;
            if (elapsed < duration)
            {
                blinkTween = LeanTween.delayedCall(blinkInterval, ToggleLoop);
            }
            else
            {
                // Ensure it stays visible after blinking ends
                if (targetImage != null)
                    targetImage.enabled = false;
            }
        }

        ToggleLoop();
    }

}

[System.Serializable]
public class SpawnPointData
{
    public Transform initialPosition;
    public Transform targetPosition;
}