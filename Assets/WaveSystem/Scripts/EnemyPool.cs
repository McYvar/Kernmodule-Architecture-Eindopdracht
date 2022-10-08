using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyPool
{
    public Enemy[] enemies;

    public EnemyPool(List<Waves> _allWaves, Vector3 _offScreenLocation)
    {
        Dictionary<SO_BaseEnemyProperties, int> tempDictForComparisons = new Dictionary<SO_BaseEnemyProperties, int>();

        for (int i = 0; i < _allWaves.Count; i++)
        {
            List<EnemyStruct> currentEnemyTypeList = _allWaves[i].enemyTypeAndAmount;
            for (int j = 0; j < currentEnemyTypeList.Count; j++)
            {
                if (tempDictForComparisons.ContainsKey(currentEnemyTypeList[j].enemyProperties))
                {
                    if (tempDictForComparisons[currentEnemyTypeList[j].enemyProperties] < currentEnemyTypeList[j].amount)
                        tempDictForComparisons[currentEnemyTypeList[j].enemyProperties] = currentEnemyTypeList[j].amount;
                }
                else
                {
                    tempDictForComparisons.Add(currentEnemyTypeList[j].enemyProperties, currentEnemyTypeList[j].amount);
                }
            }
        }
        int poolSize = 0;
        foreach (SO_BaseEnemyProperties enemyProperties in tempDictForComparisons.Keys)
        {
            poolSize += tempDictForComparisons[enemyProperties];
        }
        enemies = new Enemy[poolSize];

        int iterator = 0;

        foreach (SO_BaseEnemyProperties enemyProperty in tempDictForComparisons.Keys)
        {
            for (int i = 0; i < tempDictForComparisons[enemyProperty]; i++)
            {
                enemies[iterator] = new Enemy(enemyProperty, _offScreenLocation);
                iterator++;
            }
        }
    }

    public void Init(SO_BaseEnemyProperties _enemyProperties, Vector3[] _spawnPoints)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (!enemies[i].inUse())
            {
                if (enemies[i].enemyProperties == _enemyProperties)
                {
                    enemies[i].Init(SpawnPoint(_spawnPoints));
                    return;
                }
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