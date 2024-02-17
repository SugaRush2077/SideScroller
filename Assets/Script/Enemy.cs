using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float leftEdge;
    public float approachingSpeed = 1.1f;

    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 3f;
    }
    private void Update()
    {
        transform.position += Vector3.left * GameManager.Instance.gameSpeed * Time.deltaTime * approachingSpeed;

        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }
}

