using UnityEngine;

public class E_Game : MonoBehaviour
{

    float touchesPrevPosDifference, touchesCurrPosDifference, zoomModifier;

    Vector2 firstTouchPrevPos, secondTouchPrevPos;

    float zoomModifierSpeed = 0.1f;

    public int maxZoom = 3;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
            secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

            touchesPrevPosDifference = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
            touchesCurrPosDifference = (firstTouch.position - secondTouch.position).magnitude;

            zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * zoomModifierSpeed;

            if (touchesPrevPosDifference < touchesCurrPosDifference)
            {
                Camera.main.orthographicSize -= zoomModifier;
            }
        }
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, maxZoom, 10f);

        if (Camera.main.orthographicSize <= maxZoom)
        {
            GameManager.Instance.WinMiniGame();
            gameObject.SetActive(false);
        }
    }
}
