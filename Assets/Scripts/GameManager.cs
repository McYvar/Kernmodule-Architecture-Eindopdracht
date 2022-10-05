using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    InputHandler InputSystem;
    public GameObject actor;
    WASDCommands command = new WASDCommands();


    // Enemy spawning stuff
    [Space(20), Header("Enemy management"), Space(5)]
    [SerializeField] private int enemyPoolSize;
    [SerializeField] private ScriptableEnemy scriptableEnemy1;

    [SerializeField] Vector3 offscreenLocation;
    [SerializeField] Vector3[] enemySpawnPoints;

    private EnemyPool enemyPool;



    // Start is called before the first frame update
    void Start()
    {
        InputSystem = new InputHandler(actor);
        InputSystem.SetHandler(command);

        enemyPool = new EnemyPool(scriptableEnemy1, enemyPoolSize, offscreenLocation);
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
        }

        if (Input.GetKeyDown(KeyCode.F)) enemyPool.Init(scriptableEnemy1, enemySpawnPoints);

    }

}
