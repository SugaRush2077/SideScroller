using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{

    private float up = 2.0f;
    private float down = -2.0f;

    private void Start()
    {
        
    }
    private void Update()
    {
        float randomPos = Random.Range(down, up);
        transform.position += Vector3.up * GameManager.Instance.gameSpeed * Time.deltaTime * randomPos;

        
    }
}
