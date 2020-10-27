using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawnerService : MonoSingletonGeneric<EnemySpawnerService>
{
    public EnemyController enemy;
    public List<TankTypesScriptable> enemyType;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            EnemySpawnerService.Instance.SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, (int)TankType.OnePunchMan);
        Instantiate(enemy).Initialize(enemyType[randomIndex]);
    }
}
