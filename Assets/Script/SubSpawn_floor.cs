using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubSpawn_floor : MonoBehaviour
{
    public GameObject subspawn_roof1;
    public GameObject subspawn_roof2;
    public float threshold = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        float p = Random.Range(0f, 1f);
        if (p > threshold)
        {
            GameObject t = Instantiate(subspawn_roof1);
            t.transform.position += transform.position;
        }
        else
        {
            GameObject t = Instantiate(subspawn_roof2);
            t.transform.position += transform.position;
        }
    }
}
