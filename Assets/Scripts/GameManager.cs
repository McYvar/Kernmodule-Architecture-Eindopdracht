using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    InputHandler InputSystem;
    public GameObject actor;
    WASDCommands command = new WASDCommands();


    // Enemy spawning stuff
    [Space(20), Header("Enemy management"), Space(5)]

    [SerializeField] Vector3 offscreenLocation;
    [SerializeField] Vector3[] enemySpawnPoints;

    private EnemyPool enemyPool;

    [Space(5)]
    public List<Waves> waves;
    [SerializeField] private int currentWave;

    // Start is called before the first frame update
    void Start()
    {
        InputSystem = new InputHandler(actor);
        InputSystem.SetHandler(command);

        enemyPool = new EnemyPool(waves, offscreenLocation);
        //currentWave = 0;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyUpdate();
    }

    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            InputSystem.HandleInput(e.keyCode);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        foreach(Vector3 location in enemySpawnPoints)
        {
            Gizmos.DrawSphere(location, 0.3f);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(offscreenLocation, 0.3f);
    }

    private void EnemyUpdate()
    {
        foreach (Enemy enemy in enemyPool.enemies)
        {
            enemy.behaviour(actor.transform);


            // Temp till collision is implemented
            if (Input.GetKeyDown(KeyCode.Space)) enemy.TakeDamage(100);
        }

        // Temp till automazation
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentWave > waves.Count) return;
            waves[currentWave].SpawnWave(enemyPool, enemySpawnPoints);
            currentWave++;
        }
    }
}
