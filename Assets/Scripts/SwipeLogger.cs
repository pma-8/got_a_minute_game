using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeLogger : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
    }

    public void SwipeDetector_OnSwipe(SwipeData data)
    {
        Debug.Log("Swipe in Direction: " + data.Direction);
    }
}
