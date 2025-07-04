using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpaceCraftUIManager : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private TextMeshProUGUI speedTxt;
    [SerializeField] private TextMeshProUGUI maxSpeedTxt;
    [SerializeField] private TextMeshProUGUI rangeRemainingTxt;
    [SerializeField] private TextMeshProUGUI altitudeTxt;
    [SerializeField] private TextMeshProUGUI missileCountTxt;
    [SerializeField] private Button backToRoomBtn;
    private int maxMissile;
    private int maxRange;

    private SpacecraftController spacecraftController;
    private SpacecraftScriptable spacecraftScriptable;

    private void Start()
    {
        backToRoomBtn.onClick.AddListener(BackToRoom);
        spacecraftController = GameService.Instance.spacecraftService.GetSpacecraftController();
        spacecraftScriptable = spacecraftController.GetSpacecraftScriptable();
        maxSpeedTxt.SetText(spacecraftScriptable.maxSpeed.ToString());
        maxMissile = (spacecraftScriptable.missileCapacity);
        maxRange = spacecraftScriptable.maxRange;
    }

    public void SetMissileCount(int value)
    {
        missileCountTxt.SetText($"{value}/{maxMissile}");
    }

    public void SetAltitude(int height)
    {
        altitudeTxt.SetText(height.ToString());
    }

    public void SetRangeRemaining(int range)
    {
        rangeRemainingTxt.SetText($"{range}/{maxRange}");
    }

    public void SetSpeed(int speed)
    {
        speedTxt.SetText(speed.ToString());
    }

    public void BackToRoom()
    {
        GameService.Instance.spacecraftService.GetSpacecraftController().Deactivate();
        UIManager.Instance.SpaceCraftSelectionPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ToggleBackToRoomBtn(bool enable)
    {
        backToRoomBtn.gameObject.SetActive(enable);
    }
}
