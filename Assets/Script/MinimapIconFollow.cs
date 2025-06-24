using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapIconFollow : MonoBehaviour
{
    [SerializeField] private RectTransform minimapIcon; // Icon inside minimap RawImage
    [SerializeField] private Camera minimapCamera;
    private Transform targetToFollow; // e.g., Player

    void Update()
    {
        if (targetToFollow == null) return;
        Vector3 viewportPos = minimapCamera.WorldToViewportPoint(targetToFollow.position);

        // Convert viewport to minimap rect size
        Vector2 minimapSize = ((RectTransform)minimapIcon.parent).sizeDelta;
        minimapIcon.anchoredPosition = new Vector2(
            (viewportPos.x - 0.5f) * minimapSize.x,
            (viewportPos.y - 0.5f) * minimapSize.y
        );

        // Optional: rotate icon to match player's rotation
        minimapIcon.localEulerAngles = new Vector3(0, 0, -targetToFollow.eulerAngles.y);
    }

    public void SetTarget(Transform target)
    {
        targetToFollow = target;
    }
}

