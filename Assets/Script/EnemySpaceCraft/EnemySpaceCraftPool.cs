using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpaceCraftPool : GenericObjectPool<EnemySpaceCraftController>
{
    private EnemySpaceCraftScriptable enemySpaceCraftScriptable;

    public void Initialize(EnemySpaceCraftScriptable enemySpaceCraftScriptable)
    {
        this.enemySpaceCraftScriptable = enemySpaceCraftScriptable;
        EnemySpaceCraftController enemySpaceCraftController = PreloadItems<EnemySpaceCraftController>();

        enemySpaceCraftController.Deactivate();
    }

    public EnemySpaceCraftController GetEnemySpaceCraft<T>(EnemySpaceCraftScriptable enemySpaceCraftScriptable) where T : EnemySpaceCraftController
    {
        this.enemySpaceCraftScriptable = enemySpaceCraftScriptable;

        return GetItem<T>();
    }

    protected override EnemySpaceCraftController CreateItem<T>()
    {
        if (typeof(T) == typeof(EnemySpaceCraftController))
            return new EnemySpaceCraftController(enemySpaceCraftScriptable);
        else
            throw new NotSupportedException($"This type '{typeof(T)}' is not supported.");
    }
}


