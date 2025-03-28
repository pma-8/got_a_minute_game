using System.Collections.Generic;
using UnityEngine;

public class B_ObjectSpawner : MonoBehaviour
{
    public GameObject plane;
    public ObjectPooler bubblePooler;
    public int AmountOfBubbles = 3;
    public GameObject pooledObject;
    public List<GameObject> bubbles;

    //Plane x properties
    int _dimX;

    //Max tries finding a spawn location
    int _maxTries = 50;

    // Start is called before the first frame update
    void Start()
    {
        //Calculate dimensions of plane
        Mesh planeMesh = plane.GetComponent<MeshFilter>().mesh;
        _dimX = (int)(planeMesh.bounds.size.x * plane.transform.localScale.x);

        for(int i = 0; i < AmountOfBubbles; i++)
        {
            GameObject bubble = bubblePooler.GetPooledObject();
            SetupBubbles(bubble, i);
            bubble.SetActive(true);
        }
    }

    void SetupBubbles(GameObject pBubble, int pI)
    {
        B_ObjectBubble bubbleFloat = pBubble.GetComponent<B_ObjectBubble>();

        bubbleFloat.speed = Random.Range(4, 7);
        bubbleFloat.amplitudeMin = Random.Range(1, 4);
        bubbleFloat.amplitudeMax = Random.Range(5, 8);
        bubbleFloat.frequence = Random.Range(8, 13);

        bubbles.Add(pBubble);
        SpawnBubble(pBubble);
    }

    void SpawnBubble(GameObject pBubble)
    {
        //Check for collision while spawning objects
        //Re-calculate position if yes
        Vector3 rndPos;
        Collider2D[] hitColliders;
        bool colliding;
        int tries = 0;
        do
        {
            colliding = false;
            rndPos = RandomizePos();
            hitColliders = Physics2D.OverlapCircleAll(rndPos, 1f);

            foreach (Collider2D coll in hitColliders)
            {
                if (coll.gameObject.name.Contains(pooledObject.name))
                {
                    colliding = true;
                }
            }
            tries++;
        } while (colliding && tries <= _maxTries);

        //Don't spawn after unsuccesfully finding a safe location to spawn
        if (tries <= _maxTries)
        {
            pBubble.transform.position = rndPos;
        }
    }

    //Find randomized position at location
    public Vector3 RandomizePos()
    {
        Vector3 rndPos = Vector3.zero;
        rndPos.x = Random.Range(-(_dimX / 2f), _dimX / 2f);
        rndPos.y = gameObject.transform.position.y;
        rndPos.z = gameObject.transform.position.z;
        return rndPos;
    }
}
