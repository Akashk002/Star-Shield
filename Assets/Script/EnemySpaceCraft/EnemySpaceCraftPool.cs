using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpaceCraftPool : GenericObjectPool<EnemySpaceCraftController>
{
    private EnemySpaceCraftView enemySpaceCraftView;
    private EnemySpaceCraftScriptable enemySpaceCraftScriptable;


    public EnemySpaceCraftController GetEnemySpaceCraft<T>(EnemySpaceCraftView enemySpaceCraftView, EnemySpaceCraftScriptable enemySpaceCraftScriptable) where T : EnemySpaceCraftController
    {
        this.enemySpaceCraftView = enemySpaceCraftView;
        this.enemySpaceCraftScriptable = enemySpaceCraftScriptable;
        return GetItem<T>();
    }

    protected override EnemySpaceCraftController CreateItem<T>()
    {
        if (typeof(T) == typeof(EnemySpaceCraftController))
            return new EnemySpaceCraftController(enemySpaceCraftView, enemySpaceCraftScriptable);
        else
            throw new NotSupportedException($"This type '{typeof(T)}' is not supported.");
    }
}
