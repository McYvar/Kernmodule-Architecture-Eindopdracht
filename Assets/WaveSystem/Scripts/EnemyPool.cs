using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyPool
{
    public Enemy[] enemies;
    private List<int> ListForSpawningRandomEnemies;

    public EnemyPool(List<Waves> _allWaves, Vector3 _offScreenLocation)
    {
        ListForSpawningRandomEnemies = new List<int>();
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

    // Function that re-iterates till finding a random available enemy
    public void SpawnRandomFromPool(Vector3[] _spawnPoints, int _overflowPrevention)
    {
        _overflowPrevention++;
        Debug.Log(_overflowPrevention);
        if (_overflowPrevention > enemies.Length)
        {
            ListForSpawningRandomEnemies.Clear();
            return;
        }
        int random = GetRandomExcluding(0, enemies.Length, ListForSpawningRandomEnemies);
        ListForSpawningRandomEnemies.Add(random);
        if (enemies[random].inUse())
        {
            SpawnRandomFromPool(_spawnPoints, _overflowPrevention);
        }
        else
        {
            enemies[random].Init(SpawnPoint(_spawnPoints));
            ListForSpawningRandomEnemies.Clear();
        }
    }

    // Also a re-iterating function that recieves a list of integers that are excluded from being picked at random in range
    public int GetRandomExcluding(int _minInclusive, int _maxExclusive, List<int> _excludingIntegers)
    {
        int temp = Random.Range(_minInclusive, _maxExclusive);
        if (_excludingIntegers.Contains(temp))
        {
            return GetRandomExcluding(_minInclusive, _maxExclusive, _excludingIntegers);
        }
        else return temp;
    }
}