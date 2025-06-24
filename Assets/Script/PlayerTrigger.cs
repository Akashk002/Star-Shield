using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] private PlayerView playerView;
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        playerView.OnPlayerTriggerEnter(other.gameObject);
    }
    private void OnTriggerStay(UnityEngine.Collider other)
    {
        playerView.OnPlayerTriggerStay(other.gameObject);
    }

    private void OnTriggerExit(UnityEngine.Collider other)
    {
        playerView.OnPlayerTriggerExit(other.gameObject);
    }
}
