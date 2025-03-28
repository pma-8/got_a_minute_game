using UnityEngine;

public class D_Game : MonoBehaviour
{
    public GameObject leftWindow;
    public GameObject rightWindow;
    public GameObject roundTimer;

    public SwipeLogger swipeLogger;
    public SwipeDrawer swipeDrawer;

    public float animSpeed = 0.3f;

    private bool end = false;

    private int windowCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        SwipeDetector.OnSwipe += OpenWindow;
    }

    // Update is called once per frame
    void Update()
    {
        RoundCleanUp();

        if(windowCount >= 2)
        {
            GameManager.Instance.WinMiniGame();
        }
    }

    private void OpenWindow(SwipeData data)
    {
        if (data.Direction == SwipeDirection.Left)
        {
            LeanTween.moveLocalX(leftWindow, -3, animSpeed);
        }
        else if(data.Direction == SwipeDirection.Right)
        {
            LeanTween.moveLocalX(rightWindow, 3, animSpeed);
        }
        windowCount++;
    }

    private void RoundCleanUp()
    {
        if (!roundTimer.activeSelf && end)
        {
            SwipeDetector.OnSwipe -= OpenWindow;
            SwipeDetector.OnSwipe -= swipeLogger.SwipeDetector_OnSwipe;
            SwipeDetector.OnSwipe -= swipeDrawer.SwipeDetector_OnSwipe;
        }

        if (roundTimer.activeSelf)
        {
            end = true;
        }
    }
}
