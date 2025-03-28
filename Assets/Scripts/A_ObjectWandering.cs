using UnityEngine;

public class A_ObjectWandering : MonoBehaviour
{
    public int speed = 1;
    Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        //Give velocity with random speed and pick random direction
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        rb2D.velocity = Random.insideUnitCircle.normalized * 5;
    }
}
