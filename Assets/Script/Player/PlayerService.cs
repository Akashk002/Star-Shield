using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerService
{
    private PlayerController playerController;

    public PlayerService(PlayerView playerView, PlayerScriptable playerScriptable)
    {
        playerController = new PlayerController(playerView, playerScriptable);
    }

    public PlayerController GetPlayerController()
    {
        return playerController;
    }
}
