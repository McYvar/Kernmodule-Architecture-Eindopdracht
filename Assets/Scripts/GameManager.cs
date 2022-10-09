using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    StateMachine<int> AudioStateMachine = new StateMachine<int>();
    public CommandSystem CommandSystem;
	public GameObject actor;
    public Slider Inputvisual;
    public Text Killcount;
    public AudioSource Audio;
    public static int killcount = 0;
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
        AudioState state = new AudioState(Audio, "objectbehavior",0);
        AudioStateMachine.AddState(state);
        AudioStateMachine.SetCurrentState(state);
         enemyPool = new EnemyPool(waves, offscreenLocation);
        currentWave = 0;

        ICommand ComMsg = new PlayerCommand(actor.transform,enemyPool);
        CommandSystem.Instance.SetHandler(ComMsg);
        ICommand space = new keyTransformCommand(KeyCode.Space, Vector3.up, actor.transform);
        CommandSystem.Instance.AddCommand(space, KeyCode.Space);
    }

    // Update is called once per frame
    void Update()
    {
	   if(Input.GetMouseButtonDown(0)) //convert mouse input to keycodes
        {
            CommandSystem.Instance.HandleInput(KeyCode.Mouse0);
        }
        AudioStateMachine.Update();
        EnemyUpdate();
        CommandSystem.Instance.UpdateParticleCollision();
        UpdateGUI();
    }
    
    void OnGUI() //currently used to update keybindings, no mouse support
	{
		Event e = Event.current;
		if (e.isKey)
		{ 
			CommandSystem.Instance.HandleInput(e.keyCode);
		}
       

    }
    public void UpdateGUI()
    {
        Inputvisual.value = CommandSystem.InputCounter;
        if (Inputvisual.value >= 100)
        {
            CommandSystem.InputModifier++;
            CommandSystem.InputCounter = 0;
        }
        Killcount.text = killcount.ToString();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (enemySpawnPoints.Length <= 0) return;
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
            //if (enemy.CheckBulletInRange())
            //{
            //}
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
