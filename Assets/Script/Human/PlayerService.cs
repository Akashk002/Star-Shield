using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerService
{
    private PlayerController playerController;

    public PlayerService(PlayerView playerView, float walkSpeed, float runSpeed)
    {
        playerController = new PlayerController(playerView, walkSpeed, runSpeed);
    }

    public PlayerController GetPlayerController()
    {
        return playerController;
    }
}
