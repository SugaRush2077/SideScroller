using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float leftEdge;
    public float baseSpeed = 1.1f;
    private float approachingSpeed = 1;

    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 3f;
    }
    private void Update()
    {
        approachingSpeed = baseSpeed * GameManager.Instance.difficultyFactor;
        transform.position += Vector3.left * GameManager.Instance.gameSpeed * Time.deltaTime * approachingSpeed;

        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }
}

