using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foreground : MonoBehaviour
{
    private float leftEdge;
    public float baseSpeed = 1.1f;

    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 4f;
    }
    private void Update()
    {
        transform.position += Vector3.left * GameManager.Instance.gameSpeed * Time.deltaTime * baseSpeed;

        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }
}
