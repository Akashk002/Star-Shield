using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour, IInteractable
{
    [SerializeField] private RockType rockType;
    public void Interact()
    {
        GameService.Instance.playerController.AddRock(rockType);
        UIManager.Instance.GetInfoHandler().HideTextPopup();
        gameObject.SetActive(false);
    }

    public RockType GetRockType()
    {
        return rockType;
    }

    public string GetName()
    {
        return rockType + " Rock";
    }
}

public enum RockType
{
    Volcanis,
    Xylora,
    Cryon,
    Droska,
    Zenthra,
    Prime
}