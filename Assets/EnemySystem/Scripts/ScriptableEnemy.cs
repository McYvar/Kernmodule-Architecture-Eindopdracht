using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyList/enemy")]
public class ScriptableEnemy : ScriptableObject
{
    // Scriptable enemy can be used to create types of enemies using these variables here, or be used as base to create new sorts of enemies
    public GameObject enemyObject;
    public string enemyName;
    public int damage;
    public int speed;
    public int hp;
}
