using UnityEngine;

public class C_BottleCapWinning : MonoBehaviour
{
    public int counter = 3;
    public GameObject arrow;
    public GameObject roundTimer;
    public SwipeLogger swipeLogger;
    public SwipeDrawer swipeDrawer;

    private float animTime = 0.3f;
    private int i = 1;
    private SwipeDirection swipeDir;
    private int flyOffDir = 50;
    private int flyOffRotation = -360;

    private bool end = false;

    public LevelInformation levelInfo;

    private void Awake()
    {
        DetermineSwipeDirection();
        SwipeDetector.OnSwipe += SwipeOpenBottle;
    }

    private void Update()
    {
        RoundCleanUp();
    }

    private void RoundCleanUp()
    {
        if (!roundTimer.activeSelf && end)
        {
            SwipeDetector.OnSwipe -= SwipeOpenBottle;
            SwipeDetector.OnSwipe -= swipeLogger.SwipeDetector_OnSwipe;
            SwipeDetector.OnSwipe -= swipeDrawer.SwipeDetector_OnSwipe;
        }

        if (roundTimer.activeSelf)
        {
            end = true;
        }
    }

    private void DetermineSwipeDirection()
    {
        int rnd = Random.Range(0, 101);
        if(rnd % 2 == 0)
        {
            flyOffRotation = flyOffRotation * -1;
            flyOffDir = flyOffDir * -1;
            swipeDir = SwipeDirection.Left;
            arrow.transform.Rotate(0, 0, 180);
        }
        else
        {
            swipeDir = SwipeDirection.Right;
        }
    }

    void OpenBottle()
    {
        if (i == counter)
        {
            OpenCap(true);
        }
        else
        {
            OpenCap(false);
        }
    }

    void OpenCap(bool last)
    {
        LeanTween.moveLocalY(gameObject, gameObject.transform.position.y + 0.25f, animTime);
        if (last)
        {
            LeanTween.rotateAroundLocal(gameObject, Vector3.up, 120, animTime).setOnComplete(FallOff);
        }
        else
        {
            LeanTween.rotateAroundLocal(gameObject, Vector3.up, 120, animTime);
        }
    }

    void FallOff()
    {
        KeepRotating();
        if(gameObject.GetComponent<Rigidbody2D>() == null)
        {
            Rigidbody2D rb2D = gameObject.AddComponent<Rigidbody2D>();
            rb2D.AddForce(new Vector2(flyOffDir, 400));
        }
        else
        {
            Rigidbody2D rb2D = gameObject.GetComponent<Rigidbody2D>();
            rb2D.AddForce(new Vector2(flyOffDir, 400));
        }
        GameManager.Instance.WinMiniGame();
    }
    
    void KeepRotating()
    {
        LeanTween.rotateAroundLocal(gameObject, Vector3.forward, flyOffRotation, animTime).setOnComplete(KeepRotating);
    }

    private void SwipeOpenBottle(SwipeData data)
    {
       if(i <= counter && data.Direction == swipeDir)
        {
            OpenBottle();
            i++;
        }
        arrow.SetActive(false);
    }
}
