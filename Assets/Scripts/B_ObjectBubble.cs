using UnityEngine;

public class B_ObjectBubble : MonoBehaviour
{
    Rigidbody2D rb2D;
    public int speed = 5;
    public int amplitudeMin = 1;
    public int amplitudeMax = 5;
    public int frequence = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        rb2D.AddForce(new Vector2(0, speed), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = new Vector2(Mathf.Sin(Time.time * frequence) * Random.Range(amplitudeMin, amplitudeMax), speed);
        rb2D.velocity = dir;


        //For Touch Input
        for (int i = 0; i < Input.touchCount; i++)
        {
            //ScreenToWorldPoint always need a depth or else the camera position will be returned.
            Vector3 touchPos = new Vector3(Input.GetTouch(i).position.x, Input.GetTouch(i).position.y, 20);
            touchPos = Camera.main.ScreenToWorldPoint(touchPos);

            //Construct a ray from the current touch coordinates
            RaycastHit2D hit = Physics2D.Raycast(touchPos, new Vector3(0, 0, 0.1f));
            if (hit.collider && hit.collider.gameObject == gameObject && Input.GetTouch(i).phase == TouchPhase.Began)
            {
                gameObject.SetActive(false);
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
                gameObject.SetActive(false);
            }
        }

    }
}
