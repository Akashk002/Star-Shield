using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : GenericMonoSingleton<GameService>
{
    [SerializeField] private PlayerView playerView;
    [SerializeField] private PlayerScriptable PlayerScriptable;
    [SerializeField] private DroneView droneView;
    [SerializeField] private DroneScriptable droneScriptable;
    [SerializeField] private List<SpacecraftData> spacecraftDatas = new List<SpacecraftData>();

    public PlayerController playerController { get; private set; }
    public DroneController droneController { get; private set; }
    public SpacecraftService spacecraftService { get; private set; }
    //public EventService eventService { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        playerController = new PlayerController(playerView, PlayerScriptable);
        //spacecraftService = new SpacecraftService(spacecraftDatas);
        // droneController = new DroneController(droneView, droneScriptable);
        //eventService = new EventService();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            playerController.CarryBagPack();
        }

    }
}

public enum State
{
    Activate,
    deactivate
}