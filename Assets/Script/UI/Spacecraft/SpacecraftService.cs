using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacecraftService : MonoBehaviour
{
    private List<SpacecraftData> spacecraftDatas;
    SpacecraftController spacecraftController;

    public SpacecraftService(List<SpacecraftData> spacecraftDatas)
    {
        this.spacecraftDatas = spacecraftDatas;



        spacecraftController = new SpacecraftController(spacecraftDatas[0].spacecraftScriptable);

    }
}
