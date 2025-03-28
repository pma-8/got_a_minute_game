using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    //Object we use
    public GameObject pooledObject;

    //Amount of objects we pool
    public int pooledAmount;

    //List of our pooled objects
    private List<GameObject> _pooledObjects = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject,this.transform);
            obj.SetActive(false);
            _pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < _pooledObjects.Count; i++)
        {
            //Is object not active in scene?
            if (!_pooledObjects[i].activeInHierarchy)
            {
                return _pooledObjects[i];
            }
        }
        GameObject obj = (GameObject)Instantiate(pooledObject, this.transform);
        obj.SetActive(false);
        _pooledObjects.Add(obj);
        return obj;
    }
}
