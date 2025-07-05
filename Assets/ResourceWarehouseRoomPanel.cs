using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceWarehouseRoomPanel : MonoBehaviour
{
    private int buildingtIndex = 0;
    private BuildingScriptable buildingScriptable;

    [Header("SpaceCraft Data")]
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI decription;


    [Header("Selection data")]
    [SerializeField] private Button selectButton;
    [SerializeField] TextMeshProUGUI buildingStatus;
    [SerializeField] private GameObject costPanel;
    [SerializeField] TextMeshProUGUI rockCount;

    private List<BuildingData> buildingDatas;

    void Start()
    {
        selectButton.onClick.AddListener(OnSelectButtonClicked);
        buildingDatas = GameService.Instance.buildingManager.GetBuildingDatas();
        SetCurrentBuildingScriptable();
        SetBuildingData();
    }

    private void SetCurrentBuildingScriptable()
    {
        buildingScriptable = buildingDatas[buildingtIndex].buildingScriptable;
    }

    public void OnNextButtonClicked()
    {
        GameService.Instance.audioManager.PlayOneShotAt(GameAudioType.ClickButton, transform.position);
        buildingtIndex++;
        if (buildingtIndex >= buildingDatas.Count)
        {
            buildingtIndex = 0;
        }
        SetCurrentBuildingScriptable();
        SetBuildingData();
    }

    public void OnPreviousButtonClicked()
    {
        GameService.Instance.audioManager.PlayOneShotAt(GameAudioType.ClickButton, transform.position);
        buildingtIndex--;
        if (buildingtIndex < 0)
        {
            buildingtIndex = buildingDatas.Count - 1;
        }
        SetCurrentBuildingScriptable();
        SetBuildingData();
    }


    private void SetBuildingData()
    {
        image.sprite = buildingScriptable.buildingIcon;
        name.text = SplitCamelCase(buildingScriptable.buildingType.ToString());
        decription.text = buildingScriptable.description;
        SetSelectButtonText();
    }

    private void SetSelectButtonText()
    {
        if (buildingScriptable.buildingState == BuildingState.Locked)
        {
            buildingStatus.enabled = false;
            costPanel.SetActive(true);
            rockCount.text = buildingScriptable.rockRequire.ToString();
        }
        else
        {
            buildingStatus.enabled = true;
            costPanel.SetActive(false);
            buildingStatus.SetText(buildingScriptable.buildingState.ToString());
        }
    }

    private bool CanPuchase()
    {
        return true;
        // return false;
    }

    private void OnSelectButtonClicked()
    {
        if (buildingScriptable.buildingState == BuildingState.Locked && CanPuchase())
        {
            buildingScriptable.buildingState = BuildingState.Unlocked;
            GameService.Instance.audioManager.PlayOneShotAt(GameAudioType.Select, transform.position);
        }
    }

    public static string SplitCamelCase(string input)
    {
        return System.Text.RegularExpressions.Regex.Replace(
            input,
            "(\\B[A-Z])",
            " $1"
        );
    }
}
