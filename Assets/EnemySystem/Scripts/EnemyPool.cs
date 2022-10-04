using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool
{
    public Enemy[] enemies;

    public EnemyPool(ScriptableEnemy _scriptableEnemy, Vector3 _spawnPoint, int _poolSize)
    {
        enemies = new Enemy[_poolSize];

        for (int i = 0; i < enemies.Length; i++)
        {
            if (!enemies[i].inUse())
            {
                enemies[i].enemyObject = Object.Instantiate(_scriptableEnemy.enemyObject, _spawnPoint, Quaternion.identity);
                enemies[i].Init(_scriptableEnemy);
            }
        }
    }
}
