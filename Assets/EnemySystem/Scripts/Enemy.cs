using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    public GameObject enemyObject;
    private Vector3 spawnPoint;
    private int damage;
    private int speed;
    private int hp;

    public Enemy()
    {
        hp = 0;
    }

    public void Init(ScriptableEnemy _scriptableEnemy)
    {
        damage = _scriptableEnemy.damage;
        speed = _scriptableEnemy.speed;
        hp = _scriptableEnemy.hp;
    }

    public void behaviour(Transform _target)
    {
        if (!inUse()) return;

        enemyObject.transform.position += (_target.position - enemyObject.transform.position).normalized * speed * Time.deltaTime;
    }

    public bool inUse()
    {
        return hp > 0;
    }

    public override string ToString()
    {
        return "Enemy info: position = " + enemyObject.transform.position + ", hp = " + hp;
    }
}
