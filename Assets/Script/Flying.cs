using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{

    public float up = 3.0f;
    public float down = -1.0f;

    private void Start()
    {
        float randomPos = Random.Range(down, up);
        transform.position = new Vector3(transform.position.x, transform.position.y + randomPos, transform.position.z);
    }
    private void Update()
    {
        //float randomPos = Random.Range(down, up);
        //transform.position += Vector3.up * GameManager.Instance.gameSpeed * Time.deltaTime;

        
    }
}
