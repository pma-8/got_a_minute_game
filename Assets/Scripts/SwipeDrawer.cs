using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDrawer : MonoBehaviour
{

    private LineRenderer lineRenderer;
    private float zOffSet = 20;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
    }

    public void SwipeDetector_OnSwipe(SwipeData data)
    {
        Vector3[] positions = new Vector3[2];
        positions[0] = Camera.main.ScreenToWorldPoint(new Vector3(data.StartPosition.x, data.StartPosition.y, zOffSet));
        positions[1] = Camera.main.ScreenToWorldPoint(new Vector3(data.EndPosition.x, data.EndPosition.y, zOffSet));
        lineRenderer.positionCount = 2;
        lineRenderer.SetPositions(positions);

    }
}
