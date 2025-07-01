using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacecraftView : MonoBehaviour
{
    public Rigidbody rb;
    public CinemachineVirtualCamera cam;
    private SpacecraftController controller;
    [SerializeField] private List<Transform> shootPoints;

    public Camera Camera;

    private void Start()
    {
        Camera = Camera.main;
    }

    internal void SetController(SpacecraftController spacecraftController)
    {
        this.controller = spacecraftController;
    }

    private void Update()
    {
        if (controller != null)
        {
            controller.Update();
        }
    }

    public Transform GetShootTransform()
    {
        int randomIndex = Random.Range(0, shootPoints.Count);
        return shootPoints[randomIndex];
    }
}
