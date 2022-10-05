using UnityEngine;

public class GameManager : MonoBehaviour
{
    InputHandler InputSystem;
    public GameObject actor;
    WASDCommands command = new WASDCommands();


    // Enemy spawning stuff

    [Space(20), Header("Enemy management"), Space(5)]
    [SerializeField] private int enemyPoolSize;
    [SerializeField] private ScriptableEnemy scriptableEnemy1;

    private EnemyPool enemyPool;


    // Start is called before the first frame update
    void Start()
    {
        InputSystem = new InputHandler(actor);
        InputSystem.SetHandler(command);

        enemyPool = new EnemyPool(scriptableEnemy1, new Vector3(10, 0, 10), enemyPoolSize);
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Enemy enemy in enemyPool.enemies)
        {
            enemy.behaviour(actor.transform);
        }
    }


    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            InputSystem.HandleInput(e.keyCode);
        }
    }
}
