using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpaceCraftView : MonoBehaviour
{
    private EnemySpaceCraftController enemySpaceCraftController;
    private GameObject bulletPrefab;
    private Transform shootPoint;
    private float shootInterval = 3f;
    private Coroutine shootCoroutine;

    // Update is called once per frame
    void Update()
    {
        enemySpaceCraftController.Update();
    }

    public void SetController(EnemySpaceCraftController enemySpaceCraftController)
    {
        this.enemySpaceCraftController = enemySpaceCraftController;
    }

    public void startShooting()
    {
        shootCoroutine = StartCoroutine(ShootLoop());
    }

    private IEnumerator ShootLoop()
    {
        while (true)
        {
            enemySpaceCraftController.Shoot();
            yield return new WaitForSeconds(shootInterval);
        }
    }


    private void OnDisable()
    {
        if (shootCoroutine != null)
            StopCoroutine(shootCoroutine);
    }
}
