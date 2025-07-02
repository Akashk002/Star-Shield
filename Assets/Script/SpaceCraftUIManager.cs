using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpaceCraftUIManager : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private TextMeshProUGUI speedTxt;
    [SerializeField] private TextMeshProUGUI maxSpeedTxt;
    [SerializeField] private TextMeshProUGUI rangeRemainingTxt;
    [SerializeField] private TextMeshProUGUI altitudeTxt;
    [SerializeField] private TextMeshProUGUI missileCountTxt;
    private int maxMissile;
    private int maxRange;

    private SpacecraftController spacecraftController;
    private SpacecraftScriptable spacecraftScriptable;

    private void Start()
    {
        spacecraftController = GameService.Instance.spacecraftService.GetSpacecraftController();
        spacecraftScriptable = spacecraftController.GetSpacecraftScriptable();
        maxSpeedTxt.SetText(spacecraftScriptable.maxSpeed.ToString());
        maxMissile = (spacecraftScriptable.missileCapacity);
        maxRange = spacecraftScriptable.maxRange;
    }


    private void Update()
    {
        UpdateSpacecraftDisplayData();
    }

    public void UpdateSpacecraftDisplayData()
    {
        speedTxt.SetText(spacecraftController.GetCurrentSpeed().ToString());
        rangeRemainingTxt.SetText($"{spacecraftController.GetCurrentRange()}/{maxRange}");
        altitudeTxt.SetText(spacecraftController.GetCurrentAltitude().ToString());
        missileCountTxt.SetText($"{spacecraftController.GetMissileCount()}/{maxMissile}");
    }

    public void BackToRoom()
    {
        GameService.Instance.spacecraftService.GetSpacecraftController().Deactivate();
        UIManager.Instance.SpaceCraftSelectionPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
