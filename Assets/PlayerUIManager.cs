using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private List<RockUIData> rockUIDatas = new List<RockUIData>();
    [SerializeField] private Slider tirednessSlider;
    [SerializeField] private GridLayoutGroup stoneInfoPanel;
    [SerializeField] private Button bagPackButton;
    [SerializeField] private TextMeshProUGUI bagPackText;

    private void Start()
    {
        bagPackButton.onClick.AddListener(ToggleBagPack);
    }

    public void SetRockCount(RockType rockType, int Val)
    {
        RockUIData rockUIData = rockUIDatas.Find(data => data.rockType == rockType);
        rockUIData.SetText(Val);
    }

    public void SetTiredness(float value, float maxValue)
    {
        tirednessSlider.maxValue = maxValue;
        tirednessSlider.value = value;
    }

    public void ToggleBagPack()
    {
        if (stoneInfoPanel != null)
        {
            stoneInfoPanel.gameObject.SetActive(!stoneInfoPanel.gameObject.activeSelf);
            bagPackButton.targetGraphic.color = stoneInfoPanel.gameObject.activeSelf ? new Color(1, 1, 1, 0.5f) : Color.white;
            bagPackText.text = stoneInfoPanel.gameObject.activeSelf ? "Click to drop the bag" : "Click to get the bag";
            GameService.Instance.playerController.CarryBagPack();
        }
    }
}

[System.Serializable]
public class RockUIData
{
    public RockType rockType;
    public TMP_Text rockNameText;

    public void SetText(int count)
    {
        rockNameText.SetText(count.ToString());
    }
}

