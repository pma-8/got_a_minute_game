using UnityEngine;

public class FlyingOff : MonoBehaviour
{
    public float frequency = 5f;
    public float magnitude = 4;
    public float speed = 0.1f;
    public float gravityFallOff = 0.03f;

    private float startTime;

    private float journeyLength;

    private void Start()
    {
        startTime = Time.deltaTime;
        journeyLength = Vector3.Distance(gameObject.transform.position, FlyOffPos());
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.GetComponent<Rigidbody2D>().gravityScale > -3)
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale -= gravityFallOff;
        }

        float distCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distCovered / journeyLength;
        gameObject.transform.position = Vector3.Lerp( gameObject.transform.position,FlyOffPos(), fractionOfJourney);
    }

    public Vector3 FlyOffPos()
    {
        return new Vector3(Mathf.Sin(Time.time * frequency) * Mathf.Lerp(0, magnitude, 0.5f), gameObject.transform.position.y, gameObject.transform.position.z);
    }
}
