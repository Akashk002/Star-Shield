using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : GenericMonoSingleton<GameService>
{
    [SerializeField] private PlayerView playerView;
    [SerializeField] private PlayerScriptable PlayerScriptable;
    [SerializeField] private List<DroneData> droneDatas = new List<DroneData>();
    [SerializeField] private List<SpacecraftData> spacecraftDatas = new List<SpacecraftData>();
    [SerializeField] private List<EnemySpaceCraftView> enemySpaceCraftPrefabList = new List<EnemySpaceCraftView>();
    [SerializeField] private EnemySpaceCraftScriptable enemySpaceCraftScriptable;

    public PlayerService playerService { get; private set; }
    public DroneService droneService { get; private set; }
    public SpacecraftService spacecraftService { get; private set; }
    public EnemySpaceCraftService enemySpaceCraftService { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        playerService = new PlayerService(playerView, PlayerScriptable);
        droneService = new DroneService(droneDatas);
        spacecraftService = new SpacecraftService(spacecraftDatas);
        enemySpaceCraftService = new EnemySpaceCraftService(enemySpaceCraftPrefabList, enemySpaceCraftScriptable);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {

        }
    }
}

public enum State
{
    Activate,
    deactivate
}