using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpaceCraftView : MonoBehaviour
{
    [SerializeField] private List<Transform> shootPoints;
    private EnemySpaceCraftController enemySpaceCraftController;
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
            Transform initialTrans = shootPoints[Random.Range(0, shootPoints.Count)];
            Vector3 targetPos = GameService.Instance.playerService.GetPlayerController().GetPos();// Replace with actual target position logic, e.g., player position
            enemySpaceCraftController.Shoot(initialTrans, targetPos);
            yield return new WaitForSeconds(shootInterval);
        }
    }


    private void OnDisable()
    {
        if (shootCoroutine != null)
            StopCoroutine(shootCoroutine);
    }

    internal void TakeDamage(float damage)
    {

    }
}
