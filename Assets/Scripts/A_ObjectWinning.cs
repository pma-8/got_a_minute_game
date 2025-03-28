using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_ObjectWinning : MonoBehaviour
{
    public A_ObjectSpawner objectSpawner;
    public GameManager gameManager;

    private void Start()
    {
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", RandomizedColor.colors[0]);

        Vector3 newPos = objectSpawner.RandomizePos();
        newPos.z = newPos.z - 0.1f;
        transform.localPosition = newPos;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            //ScreenToWorldPoint always need a depth or else the camera position will be returned.
            Vector3 touchPos = new Vector3(Input.GetTouch(i).position.x, Input.GetTouch(i).position.y, 20);
            touchPos = Camera.main.ScreenToWorldPoint(touchPos);

            //Construct a ray from the current touch coordinates
            RaycastHit2D hit = Physics2D.Raycast(touchPos, new Vector3(0, 0, 0.1f));
            if (hit.collider && hit.collider.gameObject == gameObject && Input.GetTouch(i).phase == TouchPhase.Began)
            {
                gameManager.WinMiniGame();
                gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
            }
        }

        //For Mouse Input
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20);
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            //Construct a ray from the current mouse coordinates
            RaycastHit2D hit = Physics2D.Raycast(mousePos, new Vector3(0, 0, 0.1f));
            if (hit.collider && hit.collider.gameObject == gameObject)
            {
                gameManager.WinMiniGame();
                gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
            }
        }
    }
}
