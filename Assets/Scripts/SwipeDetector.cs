﻿using System;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{

    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;
    public bool detectSwipeOnlyAfterRelease = false;
    public float minDistanceForSwipe = 20f;

    public static event Action<SwipeData> OnSwipe = delegate { };

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            if(touch.phase == TouchPhase.Began)
            {
                fingerUpPosition = touch.position;
                fingerDownPosition = touch.position;
            }


            if(!detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved)
            {
                fingerDownPosition = touch.position;
                DetectSwipe();
            }

            if(touch.phase == TouchPhase.Ended)
            {
                fingerDownPosition = touch.position;
                DetectSwipe();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            fingerUpPosition = Input.mousePosition;
            fingerDownPosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            fingerDownPosition = Input.mousePosition;
            DetectSwipe();
        }
    }

    public Vector2 getFingerDownPosition()
    {
        return fingerDownPosition;
    }

    public Vector2 getFingerUpPosition()
    {
        return fingerUpPosition;
    }

    private void DetectSwipe()
    {
        if (SwipeDistanceCheckMet())
        {
            if (IsVerticalSwipe())
            {
                var direction = fingerDownPosition.y - fingerUpPosition.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
                SendSwipe(direction);
            }
            else
            {
                var direction = fingerDownPosition.x - fingerUpPosition.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
                SendSwipe(direction);
            }
            fingerUpPosition = fingerDownPosition;
        }
    }

    private bool IsVerticalSwipe()
    {
        return VerticalMovementDistance() > HorizontalMovementDistance();
    }

    private bool SwipeDistanceCheckMet()
    {
        return VerticalMovementDistance() > minDistanceForSwipe || HorizontalMovementDistance() > minDistanceForSwipe;
    }

    private float VerticalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
    }

    private float HorizontalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
    }

    private void SendSwipe(SwipeDirection direction)
    {
        SwipeData swipeData = new SwipeData()
        {
            Direction = direction,
            StartPosition = fingerDownPosition,
            EndPosition = fingerUpPosition
        };

        OnSwipe(swipeData);
    }

}

public struct SwipeData
{
    public Vector2 StartPosition;
    public Vector2 EndPosition;
    public SwipeDirection Direction;
}

public enum SwipeDirection
{
    Up,
    Down,
    Left,
    Right
}


