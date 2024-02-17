using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float spawnChance;
    }

    public SpawnableObject[] objects;

    // Spawn time in second
    // Depend on distance & speed & time
    //public float minSpawnRate = 1f;
    //public float maxSpawnRate = 1.5f;
    private float spawnPeriod = 0f;
    private int jumpDistance;

    // Calculate the distance of player can jump
    private void calculateDistance()
    {
        // Get Player Jump Angle
        // tan = jumpForce / groundSpeed (gameSpeed)
        // angle = 180 / (pi * radian)
        float pj = GameManager.Instance.player_jumpForce;
        float g = GameManager.Instance.player_gravity;
        float gs = GameManager.Instance.gameSpeed;
        float angle = Mathf.Tan(pj / gs); // Radians
        // distance = V0 * 2 * Sin (2 * angle / gravity)
        float v0 = Mathf.Sqrt(pj * pj + g * g);
        jumpDistance = Mathf.CeilToInt(v0 * 2 * Mathf.Sin(2 * angle / g)) + 1; // plus 1 for boundary
        spawnPeriod = jumpDistance / gs;
    }


    private void OnEnable()
    {
        Invoke(nameof(Spawn), Random.Range(spawnPeriod, spawnPeriod + 1 + GameManager.Instance.difficultyFactor));
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
            if (spawnProbability < obj.spawnChance)
            {
                GameObject obstacle = Instantiate(obj.prefab);
                obstacle.transform.position += transform.position;
                break;
            }

            spawnProbability -= obj.spawnChance;
        }

        Invoke(nameof(Spawn), Random.Range(spawnPeriod, spawnPeriod + 1 + GameManager.Instance.difficultyFactor));
    }
}
