using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Waves
{
    public List<EnemyStruct> enemyTypeAndAmount;

    public void SpawnWave(EnemyPool pool, Vector3[] _spawnPoints)
    {
        foreach(EnemyStruct enemy in enemyTypeAndAmount)
        {
            for (int i = 0; i < enemy.amount; i++)
            {
                pool.Init(enemy.enemyProperties, _spawnPoints);
            }
        }
    }
}

[System.Serializable]
public struct EnemyStruct
{
    public SO_BaseEnemyProperties enemyProperties;
    public int amount;
}
