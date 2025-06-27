using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpaceCraftService
{
    private List<EnemySpaceCraftView> enemySpaceCraftPrefabs;
    private EnemySpaceCraftScriptable enemySpaceCraftScriptable;
    private EnemySpaceCraftPool enemySpaceCraftPool;

    public EnemySpaceCraftService(List<EnemySpaceCraftView> enemySpaceCraftpPrefabs, EnemySpaceCraftScriptable enemySpaceCraftScriptable)
    {
        this.enemySpaceCraftPrefabs = enemySpaceCraftpPrefabs;
        this.enemySpaceCraftScriptable = enemySpaceCraftScriptable;
        enemySpaceCraftPool = new EnemySpaceCraftPool();
    }

    public void CreateEnemySpaceCraft(Vector3 initialPos, Vector3 targetPos)
    {
        EnemySpaceCraftView enemySpaceCraftView = enemySpaceCraftPrefabs[Random.Range(0, enemySpaceCraftPrefabs.Count)];
        EnemySpaceCraftController enemySpaceCraftController = enemySpaceCraftPool.GetEnemySpaceCraft<EnemySpaceCraftController>(enemySpaceCraftView, enemySpaceCraftScriptable);

        enemySpaceCraftController.Configure(initialPos, targetPos);
    }

    public void ReturnDefenderPool(EnemySpaceCraftController enemySpaceCraftController) => enemySpaceCraftPool.ReturnItem(enemySpaceCraftController);
}
