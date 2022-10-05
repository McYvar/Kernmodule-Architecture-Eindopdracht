using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool
{
    public Enemy[] enemies;

    // When creating a pool, every enemy in the pool (depending on the given pool size) should be instantiated (since we only get to use one monobehaviour)
    public EnemyPool(ScriptableEnemy _scriptableEnemy, int _poolSize, Vector3 _offScreenLocation)
    {
        enemies = new Enemy[_poolSize];

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i] = new Enemy(_scriptableEnemy, _offScreenLocation);
        }
    }

    public void Init(ScriptableEnemy _scriptableEnemy, Vector3[] _spawnPoints)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (!enemies[i].inUse())
            {
                enemies[i].Init(_scriptableEnemy, SpawnPoint(_spawnPoints));
                return;
            }
        }
        Debug.Log("no enemy was available");
    }

    Vector3 SpawnPoint(Vector3[] _spawnPoints)
    {
        int pointIndex = Random.Range(0, _spawnPoints.Length);
        return _spawnPoints[pointIndex];
    }
}
