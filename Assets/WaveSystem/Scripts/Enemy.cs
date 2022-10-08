using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy
{
    [HideInInspector] public GameObject enemyObject;
    [HideInInspector] public SO_BaseEnemyProperties enemyProperties;
    private Vector3 spawnPoint;
    private Vector3 offScreenLocation;
    private string name;
    private int damage;
    private int speed;
    private int hp;

    // Upon creating a new enemy, its hp is set to 0 so it won't be active until initialization
    public Enemy(SO_BaseEnemyProperties _enemyProperties, Vector3 _offScreenLocation)
    {
        enemyProperties = _enemyProperties;
        offScreenLocation = _offScreenLocation;
        enemyObject = Object.Instantiate(_enemyProperties.enemyObject, _offScreenLocation, Quaternion.identity);
        name = _enemyProperties.name;
        
        hp = 0;
    }

    public void Init(Vector3 _spawnpoint)
    {
        enemyObject.name = enemyProperties.enemyName;
        enemyObject.transform.position = _spawnpoint;

        damage = enemyProperties.damage;
        speed = enemyProperties.speed;
        hp = enemyProperties.hp;
    }

    // Enemy behaviour() gets called every frame for each enemy
    public bool behaviour(Transform _target)
    {
        if (!inUse())
        {
            enemyObject.name = name + " (inactive)";
            enemyObject.transform.position = offScreenLocation;
            return false;
        }

        enemyObject.name = name + " (active)";
        enemyObject.transform.position += (_target.position - enemyObject.transform.position).normalized * speed * Time.deltaTime;
        return true;
    }

    public bool inUse()
    {
        return hp > 0;
    }

    public override string ToString()
    {
        return "Enemy(" + enemyObject.name + ") info: position = " + enemyObject.transform.position + ", hp = " + hp;
    }

    public void TakeDamage(int _damage)
    {
        hp -= _damage;
    }

    // Deals damage when the enemy gets close to a given object transform, then sets own hp to 0
    public int DealDamage(Transform obj)
    {
        if (Vector3.Distance(enemyObject.transform.position, obj.position) < 1)
        {
            hp = 0;
            return damage;
        }
        return 0;
    }

    // I don't know about this yet...
    public bool CheckBulletInRange(Transform _bullet)
    {
        return Vector3.Distance(enemyObject.transform.position, _bullet.position) < 1f;
    }
}
