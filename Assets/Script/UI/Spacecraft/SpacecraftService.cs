using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacecraftService
{
    private List<SpacecraftData> spacecraftDatas = new List<SpacecraftData>();
    private SpacecraftController spacecraftController;

    public SpacecraftService(List<SpacecraftData> spacecraftDatas)
    {
        this.spacecraftDatas = spacecraftDatas;
    }

    public void CreateSpacecraft(SpacecraftType spacecraftType)
    {
        SpacecraftData spacecraftData = spacecraftDatas.Find(data => data.spacecraftType == spacecraftType);
        spacecraftController = new SpacecraftController(spacecraftData.spacecraftScriptable);
        spacecraftController.Configure();
    }

    public SpacecraftController GetSpacecraftController()
    {
        return spacecraftController;
    }
}
