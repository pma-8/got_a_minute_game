using UnityEngine;

public class F_Game : MonoBehaviour
{
    public GameObject roundTimer;

    public SwipeLogger swipeLogger;
    public SwipeDrawer swipeDrawer;

    private bool end = false;
    public float animTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        SwipeDetector.OnSwipe += PullRope;
    }

    // Update is called once per frame
    void Update()
    {
        RoundCleanUp();
    }

    void PullRope(SwipeData data)
    {
        if(data.Direction == SwipeDirection.Down)
        {
            FlyOff();
            GameManager.Instance.WinMiniGame();
        }
    }

    private void FlyOff()
    {
        Rigidbody2D rb2d = gameObject.AddComponent<Rigidbody2D>();
        rb2d.gravityScale = 4.55f;
        GetComponent<FlyingOff>().enabled = true;
    }

    private void RoundCleanUp()
    {
        if(!roundTimer.activeSelf && end)
        {
            SwipeDetector.OnSwipe -= PullRope;
            SwipeDetector.OnSwipe -= swipeLogger.SwipeDetector_OnSwipe;
            SwipeDetector.OnSwipe -= swipeDrawer.SwipeDetector_OnSwipe;
        }

        if (roundTimer.activeSelf)
        {
            end = true;
        }
    }
}
