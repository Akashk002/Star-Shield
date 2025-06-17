using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : GenericMonoSingleton<GameService>
{
    [SerializeField] float PlayerWalkSpeed;
    [SerializeField] float PlayerRunSpeed;
    [SerializeField] PlayerView playerView;
    [SerializeField] List<SpacecraftData> spacecraftDatas = new List<SpacecraftData>();
    public PlayerService playerService { get; private set; }
    public SpacecraftService spacecraftService { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        playerService = new PlayerService(playerView, PlayerWalkSpeed, PlayerRunSpeed);
        //spacecraftService = new SpacecraftService(spacecraftDatas);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
