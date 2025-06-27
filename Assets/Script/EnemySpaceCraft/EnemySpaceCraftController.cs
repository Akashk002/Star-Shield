using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class EnemySpaceCraftController
{
    private EnemySpaceCraftScriptable enemySpaceCraftScriptable;
    private EnemySpaceCraftView enemySpaceCraftView;
    private Vector3 targetPosition;

    private bool isTargetReached = false;
    private bool isMoving = false;

    public EnemySpaceCraftController(EnemySpaceCraftView enemySpaceCraftprefab, EnemySpaceCraftScriptable enemySpaceCraftScriptable)
    {
        enemySpaceCraftView = Object.Instantiate(enemySpaceCraftprefab);
        enemySpaceCraftView.SetController(this);
        this.enemySpaceCraftScriptable = enemySpaceCraftScriptable;

    }

    public void Configure(Vector3 initialPos, Vector3 tragetPos)
    {
        enemySpaceCraftView.transform.position = initialPos;
        targetPosition = tragetPos;

        LookAtTarget();
        isMoving = true;
    }

    public void Update()
    {
        if (isMoving)
        {
            MoveTowardTarget();
        }
    }

    private void LookAtTarget()
    {
        Vector3 dir = (targetPosition - enemySpaceCraftView.transform.position).normalized;
        if (dir != Vector3.zero)
            enemySpaceCraftView.transform.rotation = Quaternion.LookRotation(dir);
    }

    private void MoveTowardTarget()
    {
        enemySpaceCraftView.transform.position = Vector3.MoveTowards(enemySpaceCraftView.transform.position, targetPosition, enemySpaceCraftScriptable.moveSpeed * Time.deltaTime);
        if (!isTargetReached && Vector3.Distance(enemySpaceCraftView.transform.position, targetPosition) < 0.1f)
        {
            isTargetReached = true;
            isMoving = false;
            enemySpaceCraftView.startShooting();
        }
    }

    public void Shoot()
    {
        Debug.Log("Missile launch!");
    }
}
