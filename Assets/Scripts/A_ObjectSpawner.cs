using UnityEngine;

public class A_ObjectSpawner : MonoBehaviour
{
    public GameObject plane;
    public int AmountOfObjects;
    public GameObject pooledObject;

    ObjectPooler obstaclePooler;

    //Plane Properties
    int x_dim;
    int y_dim;

    //Max tries finding a spawn location
    int _maxTries = 20;

    // Start is called before the first frame update
    void Start()
    {

        obstaclePooler = gameObject.GetComponent<ObjectPooler>();

        //Calculate dimensions of plane
        Mesh planeMesh = plane.GetComponent<MeshFilter>().mesh;
        x_dim = (int) (planeMesh.bounds.size.x * plane.transform.localScale.x);
        y_dim = (int) (planeMesh.bounds.size.z * plane.transform.localScale.z);

        //Spawn all objects on randomized locations on plane
        for(int i = 0; i < AmountOfObjects; i++)
        {
            SpawnOnPlane(obstaclePooler);
        }
    }

    void SpawnOnPlane(ObjectPooler pObjectPooler)
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
            hitColliders = Physics2D.OverlapCircleAll(rndPos, 0.4f);

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
        if(tries <= _maxTries)
        {
            GameObject tmpObj = pObjectPooler.GetPooledObject();

            //Randomize location
            Transform instance = tmpObj.transform;
            instance.localPosition = rndPos;

            //Randomize color
            tmpObj.GetComponent<Renderer>().material.SetColor("_Color", RandomizedColor.colors[Random.Range(1, RandomizedColor.colors.Length)]);

            tmpObj.SetActive(true);
        }
    }

    //Find randomized position on location
    public Vector3 RandomizePos()
    {
        Vector3 rndPos = Vector3.zero;
        rndPos.x = (int)Random.Range(-x_dim / 2f, x_dim / 2f);
        rndPos.y = (int)Random.Range(-y_dim / 2f, y_dim / 2f);
        rndPos.z = 20f;

        return rndPos;
    }
}
