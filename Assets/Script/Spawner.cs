using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float spawnProbability;
    }

    public SpawnableObject[] objects;

    // Spawn time in second
    public float minSpawnRate = 1f; 
    public float maxSpawnRate = 2f;

    private void OnEnable()
    {
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
    private void Spawn()
    {
        float spawnProbability = Random.value;

        foreach (var obj in objects)
        {
            if (spawnProbability < obj.spawnProbability)
            {
                GameObject obstacle = Instantiate(obj.prefab);
                obstacle.transform.position += transform.position;
                break;
            }

            spawnProbability -= obj.spawnProbability;
        }

        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }
}
