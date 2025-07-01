﻿using UnityEngine;

public class MissileController : MonoBehaviour
{
    public MissileScriptable missileScriptable;
    private MissileView missileView;
    public MissileType missileType;

    private Vector3 targetPosition;
    private bool isTargetReached = false;

    private bool isLaunching = false;
    private bool isTurning = false;
    private bool isTracking = false;

    private float launchTime;
    private float turnDuration = 0.5f;
    private float timeSinceLaunch;

    private Quaternion initialRotation;
    private Quaternion targetRotation;

    private bool enableTurning = true; // <-- New flag added

    public MissileController(MissileScriptable missileScriptable)
    {
        missileView = Object.Instantiate(missileScriptable.missileView);
        missileView.SetController(this);
        this.missileScriptable = missileScriptable;
        this.missileType = missileScriptable.missileType;
    }

    // 🔧 Optionally allow disabling turn
    public void Configure(Transform initransform, Vector3 tragetPos, bool turnEnabled = true)
    {
        missileView.gameObject.SetActive(true);

        missileView.transform.position = initransform.position;
        missileView.transform.rotation = initransform.rotation;
        targetPosition = tragetPos;

        enableTurning = turnEnabled;

        timeSinceLaunch = 0f;
        isTargetReached = false;

        if (enableTurning)
        {
            isLaunching = true;

            initialRotation = initransform.rotation;
            targetRotation = Quaternion.LookRotation((targetPosition - initransform.position).normalized);
        }
        else
        {
            isLaunching = false;
            isTurning = false;
            isTracking = true; // 🔁 Go directly to tracking
        }
    }

    public void Update()
    {
        if (isLaunching)
        {
            timeSinceLaunch += Time.deltaTime;

            missileView.transform.position += missileView.transform.forward * missileScriptable.boostSpeed * Time.deltaTime;

            if (timeSinceLaunch >= 0.5f)
            {
                isLaunching = false;
                isTurning = true;
                launchTime = Time.time;
            }
        }
        else if (isTurning)
        {
            float t = (Time.time - launchTime) / turnDuration;
            missileView.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, t);

            if (t >= 1f)
            {
                isTurning = false;
                isTracking = true;
            }
        }
        else if (isTracking)
        {
            MoveTowardTarget();
        }
    }

    private void MoveTowardTarget()
    {
        Vector3 dir = (targetPosition - missileView.transform.position).normalized;
        missileView.transform.position += dir * missileScriptable.moveSpeed * Time.deltaTime;
        missileView.transform.rotation = Quaternion.LookRotation(dir);

        //if (!isTargetReached && Vector3.Distance(missileView.transform.position, targetPosition) < 0.1f)
        //{
        //    isTargetReached = true;
        //    isTracking = false;
        //}
    }

    public int GetdamageValue()
    {
        throw new System.NotImplementedException();
    }
}
