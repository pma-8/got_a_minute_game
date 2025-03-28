using UnityEngine;

public class InputTesting : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {

            Vector3 touchPos = new Vector3(Input.GetTouch(i).position.x, Input.GetTouch(i).position.y, 20);
            touchPos = Camera.main.ScreenToWorldPoint(touchPos);
            Debug.DrawRay(touchPos, new Vector3(0, 0.2f, 0));
        }

        //For Mouse Input
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20);
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            Debug.DrawRay(mousePos, new Vector3(0, 0.2f, 0));
        }
    }
}
