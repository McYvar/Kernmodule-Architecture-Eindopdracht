using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyList/enemy")]
public class ScriptableEnemy : ScriptableObject
{
    public GameObject enemyObject;
    public string enemyName;
    public int damage;
    public int speed;
    public int hp;
}
