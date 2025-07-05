using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : GenericMonoSingleton<GameService>
{
    [SerializeField] private PlayerView playerView;
    [SerializeField] private PlayerScriptable PlayerScriptable;
    [SerializeField] private List<DroneData> droneDatas = new List<DroneData>();
    [SerializeField] private List<SpacecraftData> spacecraftDatas = new List<SpacecraftData>();
    [SerializeField] private List<EnemySpaceCraftScriptable> enemySpaceCraftScriptables = new List<EnemySpaceCraftScriptable>();
    [SerializeField] private List<MissileData> missileDatas = new List<MissileData>();
    [SerializeField] private VFXView VFXPrefab;
    public BuildingManager buildingManager;
    public AudioManager audioManager;
    public PlayerService playerService { get; private set; }
    public DroneService droneService { get; private set; }
    public SpacecraftService spacecraftService { get; private set; }
    public EnemySpaceCraftService enemySpaceCraftService { get; private set; }
    public MissileService missileService { get; private set; }

    public VFXService VFXService { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        enemySpaceCraftScriptables.Shuffle();
        playerService = new PlayerService(playerView, PlayerScriptable);
        droneService = new DroneService(droneDatas);
        spacecraftService = new SpacecraftService();
        enemySpaceCraftService = new EnemySpaceCraftService(enemySpaceCraftScriptables);
        missileService = new MissileService(missileDatas);
        VFXService = new VFXService(VFXPrefab);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {

        }
    }

    public List<SpacecraftData> GetSpacecraftDatas()
    {
        return spacecraftDatas;
    }
    public List<MissileData> GetMissileDatas()
    {
        return missileDatas;
    }

    public PlayerScriptable GetPlayerScriptable()
    {
        return PlayerScriptable;
    }
}

public enum State
{
    Activate,
    deactivate
}

public static class StaticClass
{
    public static void Shuffle<T>(this List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}