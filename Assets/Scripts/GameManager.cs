using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    InputHandler InputSystem;
    public GameObject actor;
    WASDCommands command = new WASDCommands();



    [SerializeField] int playerHp; 

    // Enemy spawning stuff
    [Space(20), Header("Enemy management"), Space(5)]

    [SerializeField] Vector3 offscreenLocation;
    [SerializeField] Vector3[] enemySpawnPoints;

    private EnemyPool enemyPool;

    [Space(5)]
    public List<Waves> waves;
    [SerializeField] private int currentWave;
    private float endlessWaveRespawnTimer;
    [SerializeField, Range(0, 1.3f)] private float endlessWaveRespawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        InputSystem = new InputHandler(actor);
        InputSystem.SetHandler(command);

        enemyPool = new EnemyPool(waves, offscreenLocation);
        currentWave = 0;
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
        foreach (Vector3 location in enemySpawnPoints)
        {
            Gizmos.DrawSphere(location, 0.3f);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(offscreenLocation, 0.3f);
    }

    private void EnemyUpdate()
    {
        bool canSpawnNewEnemies = true;
        foreach (Enemy enemy in enemyPool.enemies)
        {
            if (enemy.Behaviour(actor.transform)) canSpawnNewEnemies = false;

            // Temp till collision is implemented
            if (Input.GetKeyDown(KeyCode.Space)) enemy.TakeDamage(100);
            playerHp -= enemy.DealDamage(actor.transform);
        }

        if (currentWave >= waves.Count)
        {
            // Endless mode
            Debug.Log("running endless mode");
            if (endlessWaveRespawnTimer > endlessWaveRespawnDelay)
            {
                enemyPool.SpawnRandomFromPool(enemySpawnPoints, 0);
                endlessWaveRespawnTimer = 0;
                endlessWaveRespawnDelay *= 0.99f;
            }
            endlessWaveRespawnTimer += Time.deltaTime;
        }
        else
        {
            // Pre-setup waves
            if (canSpawnNewEnemies)
            {
                Debug.Log("Spawning wave " + (currentWave + 1) + "!");
                waves[currentWave].SpawnWave(enemyPool, enemySpawnPoints);
                currentWave++;
            }
        }
    }
}
