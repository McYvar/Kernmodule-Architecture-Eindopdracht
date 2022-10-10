using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public CommandSystem commandSystem;
    public GameObject actor;
    public Slider inputvisual;
    public Text onScreenKillCount;
    public TMP_Text midScreenBigText;
    public AudioSource audio;
    public static int killCount = 0;

    private StateMachine<int> audioStateMachine = new StateMachine<int>();
    private AudioState audiostate;
    private AudioState audiostate2;
    private AudioState audiostate3;



    [SerializeField] private int playerHp;
    // Enemy spawning stuff
    [Space(20), Header("Enemy management"), Space(5)]
    [SerializeField] private Vector3 offscreenLocation;
    [Space(5), SerializeField] private Vector3[] enemySpawnPoints;
    private EnemyPool enemyPool;
    [Space(5), SerializeField] private List<Waves> waves;
    private float endlessWaveRespawnTimer;
    [SerializeField] private int currentWave;
    [SerializeField, Range(0, 1.3f)] private float endlessWaveRespawnDelay;

    private void Start()
    {
        audiosetup();
        enemyPool = new EnemyPool(waves, offscreenLocation);
        currentWave = 0;

        ICommand ComMsg = new PlayerCommand(actor.transform, enemyPool);
        CommandSystem.Instance.SetHandler(ComMsg);
        ICommand space = new keyTransformCommand(KeyCode.Space, Vector3.up, actor.transform);
        CommandSystem.Instance.AddCommand(space, KeyCode.Space);
    }

    private void Update()
    {
        if (playerHp < 0)
        {
            midScreenBigText.text = "Game Over!";
            return;
        }

        if (Input.GetMouseButtonDown(0)) //convert mouse input to keycodes
        {
            CommandSystem.Instance.HandleInput(KeyCode.Mouse0);
        }

        if (audiostate.Finished)
        {
            audioStateMachine.SetCurrentState(audiostate2);
            audiostate.OnExit();
        }
        if (audiostate2.Finished)
        {
            audioStateMachine.SetCurrentState(audiostate3);
            audiostate2.OnExit();
        }
        if (audiostate3.Finished)
        {
            audioStateMachine.SetCurrentState(audiostate);
            audiostate3.OnExit();
        }
        audioStateMachine.Update();
        EnemyUpdate();
        CommandSystem.Instance.UpdateParticleCollision();
        UpdateGUI();
    }

    private void OnGUI() //currently used to update keybindings, no mouse support
    {
        Event e = Event.current;
        if (e.isKey)
        {
            CommandSystem.Instance.HandleInput(e.keyCode);
        }
    }
    public void UpdateGUI()
    {
        inputvisual.value = CommandSystem.InputCounter;
        if (inputvisual.value >= 100)
        {
            CommandSystem.InputModifier++;
            CommandSystem.InputCounter = 0;
        }
        onScreenKillCount.text = killCount.ToString();
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
    private void audiosetup()
    {
        audiostate = new AudioState(audio, "cowReverbed", 0);
        audiostate2 = new AudioState(audio, "cow", 1);
        audiostate3 = new AudioState(audio, "cowFinal", 2);
        audioStateMachine.AddState(audiostate);
        audioStateMachine.AddState(audiostate2);
        audioStateMachine.AddState(audiostate3);
        audioStateMachine.SetCurrentState(audiostate);
    }
}
