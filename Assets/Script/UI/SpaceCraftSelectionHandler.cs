using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SpaceCraftSelectionHandler : MonoBehaviour
{
    private int SpaceCraftIndex = 0;
    private SpacecraftScriptable spacecraftScriptable;

    [Header("SpaceCraft Data")]
    [SerializeField] private Image spacecraftImage;
    [SerializeField] private TextMeshProUGUI spacecraftName;
    [SerializeField] private SliderAndText maxSpeed;
    [SerializeField] private SliderAndText maxRange;
    [SerializeField] private SliderAndText maxCapacity;


    [Header("Missile Data")]
    [SerializeField] private Image missileImage;
    [SerializeField] private TextMeshProUGUI missileName;
    [SerializeField] private SliderAndText speed;
    [SerializeField] private SliderAndText Damage;

    [Header("Selection data")]
    [SerializeField] private Button selectButton;
    [SerializeField] TextMeshProUGUI spaceCraftStatus;
    [SerializeField] private GameObject costPanel;
    [SerializeField] TextMeshProUGUI xyloraCount;
    [SerializeField] TextMeshProUGUI primeCount;

    private List<SpacecraftData> spacecraftData;
    private List<MissileData> missileData;

    void Start()
    {
        selectButton.onClick.AddListener(OnSelectButtonClicked);
        spacecraftData = GameService.Instance.GetSpacecraftDatas();
        missileData = GameService.Instance.GetMissileDatas();
        SetCurrentSpacecraftScriptable();
        SetSpaceCraftData();
    }

    private void SetCurrentSpacecraftScriptable()
    {
        spacecraftScriptable = spacecraftData[SpaceCraftIndex].spacecraftScriptable;
    }

    public void OnNextButtonClicked()
    {
        SpaceCraftIndex++;
        if (SpaceCraftIndex >= spacecraftData.Count)
        {
            SpaceCraftIndex = 0;
        }
        SetCurrentSpacecraftScriptable();
        SetSpaceCraftData();
    }

    public void OnPreviousButtonClicked()
    {
        SpaceCraftIndex--;
        if (SpaceCraftIndex < 0)
        {
            SpaceCraftIndex = spacecraftData.Count - 1;
        }
        SetCurrentSpacecraftScriptable();
        SetSpaceCraftData();
    }


    private void SetSpaceCraftData()
    {
        spacecraftImage.sprite = spacecraftScriptable.spacecraftSprite;
        spacecraftName.text = spacecraftScriptable.spacecraftType.ToString();
        maxSpeed.SetValue((int)spacecraftScriptable.maxSpeed);
        maxRange.SetValue(spacecraftScriptable.maxRange);
        maxCapacity.SetValue(spacecraftScriptable.missileCapacity);
        SetSelectButtonText();
        SetMissileData(spacecraftScriptable.missileType);
    }

    private void SetMissileData(MissileType missileType)
    {
        MissileScriptable missileData = this.missileData.Find(data => data.missileType == missileType).missileScriptable;
        if (missileData != null)
        {
            missileImage.sprite = missileData.spacecraftSprite;
            missileName.text = missileData.missileType.ToString();
            speed.SetValue((int)missileData.moveSpeed);
            Damage.SetValue((int)missileData.damage);
        }
    }

    private void SetSelectButtonText()
    {
        if (spacecraftScriptable.spacecraftStatus == SpacecraftStatus.Locked)
        {
            spaceCraftStatus.enabled = false;
            costPanel.SetActive(true);
            xyloraCount.text = spacecraftData[SpaceCraftIndex].spacecraftScriptable.Xylora_Rocks.ToString();
            primeCount.text = spacecraftData[SpaceCraftIndex].spacecraftScriptable.Prime_Rocks.ToString();
        }
        else
        {
            spaceCraftStatus.enabled = true;
            costPanel.SetActive(false);
        }

        spaceCraftStatus.SetText(spacecraftScriptable.spacecraftStatus.ToString());
    }

    private bool CanPuchase()
    {
        int playerXyloraCount = GameService.Instance.GetPlayerScriptable().rockDatas.Find(x => x.RockType == RockType.Xylora).rockCount;
        int playerPrimeCount = GameService.Instance.GetPlayerScriptable().rockDatas.Find(x => x.RockType == RockType.Prime).rockCount;
        int XyloraCount = spacecraftScriptable.Xylora_Rocks;
        int PrimeCount = spacecraftScriptable.Prime_Rocks;

        if (playerXyloraCount >= XyloraCount && playerPrimeCount >= PrimeCount)
        {
            return true;
        }

        return true;
        // return false;
    }

    private void Select()
    {
        foreach (SpacecraftData spacecraftData in spacecraftData)
        {
            if (spacecraftData.spacecraftScriptable.spacecraftStatus == SpacecraftStatus.Selected)
            {
                spacecraftData.spacecraftScriptable.spacecraftStatus = SpacecraftStatus.Unlocked;
            }
        }
        spacecraftScriptable.spacecraftStatus = SpacecraftStatus.Selected;
        SetSpaceCraftData();
    }

    private void OnSelectButtonClicked()
    {
        if (spacecraftScriptable.spacecraftStatus == SpacecraftStatus.Unlocked || spacecraftScriptable.spacecraftStatus == SpacecraftStatus.Locked && CanPuchase())
        {
            Select();
        }
    }

    public void FlySpaceCraft()
    {
        GameService.Instance.spacecraftService.CreateSpacecraft(spacecraftScriptable);
        gameObject.SetActive(false);
    }

    public void Refuel()
    {

    }

    public void ReloadMissile()
    {

    }
}
[System.Serializable]
public struct SliderAndText
{
    public Slider slider;
    public TextMeshProUGUI text;

    public void SetValue(int value)
    {
        slider.value = value;
        text.text = value.ToString();
    }
}