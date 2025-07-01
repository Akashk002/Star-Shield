using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpacecraftUIPanelController : GenericMonoSingleton<UIManager>
{
    [Header("UI Panels")]
    [SerializeField] private TextMeshProUGUI speed;
    [SerializeField] private TextMeshProUGUI maxSpeed;
    [SerializeField] private TextMeshProUGUI rangeRemaining;
    [SerializeField] private TextMeshProUGUI altitude;
    [SerializeField] private TextMeshProUGUI missileCount;
    [SerializeField] private TextMeshProUGUI maxMissileCount;


    private SpacecraftController spacecraftController;
    private SpacecraftScriptable spacecraftScriptable;

    private void Start()
    {
        spacecraftController = GameService.Instance.spacecraftService.GetSpacecraftController();
        spacecraftScriptable = spacecraftController.GetSpacecraftScriptable();
    }


    private void FixedUpdate()
    {
        UpdateSpacecraftDisplayData();
    }

    public void UpdateSpacecraftDisplayData()
    {
        speed.SetText(spacecraftController.GetCurrentSpeed().ToString());
        maxSpeed.SetText(spacecraftScriptable.maxSpeed.ToString());
        rangeRemaining.SetText(spacecraftController.GetCurrentRange().ToString());
        altitude.SetText(spacecraftController.GetCurrentAltitude().ToString());
        missileCount.SetText(spacecraftController.GetMissileCount().ToString());
        maxMissileCount.SetText(spacecraftScriptable.missileCapacity.ToString());
    }
}

