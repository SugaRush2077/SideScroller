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
    public float minSpawnRate = 1f;
    public float maxSpawnRate = 1.5f;
    private float spawnPeriod = 1f;
    private int jumpDistance;
    private float jumpForce;
    private float gravity;
    private float factor;

    // Calculate the distance of player can jump
    private void calculateDistance()
    {
        // Get Player Jump Angle
        // tan = jumpForce / groundSpeed (gameSpeed)
        // angle = 180 / (pi * radian)
        jumpForce = GameManager.Instance.player_jumpForce;
        gravity = GameManager.Instance.player_gravity;
        factor = GameManager.Instance.difficultyFactor;
        float gs = GameManager.Instance.gameSpeed;
        Debug.Log(jumpForce);
        float angle = Mathf.Tan(jumpForce / gs); // Radians
        // distance = V0 * 2 * Sin (2 * angle / gravity)
        float v0 = Mathf.Sqrt(jumpForce * jumpForce + gravity * gravity);
        //Debug.Log(v0);
        Debug.Log(factor);
        Debug.Log(gs);
        Debug.Log(angle);
        jumpDistance = Mathf.CeilToInt(v0 * 2 * Mathf.Sin(2 * angle / gravity)) + 1; // plus 1 for boundary
        spawnPeriod = jumpDistance / gs;
        //Debug.Log(jumpDistance);
        Invoke(nameof(calculateDistance), 1);
    }
    private void Start()
    {
        
        
    }

    private void OnEnable()
    {

        //Invoke(nameof(Spawn), spawnPeriod + 2);
        Invoke(nameof(calculateDistance), 1);
        Invoke(nameof(Spawn), Random.Range(3, spawnPeriod));
        //Invoke(nameof(Spawn), Random.Range(spawnPeriod, spawnPeriod + 1 + GameManager.Instance.difficultyFactor));
        //Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

    

    private void OnDisable()
    {
        CancelInvoke();
    }
    private void Spawn()
    {
        //calculateDistance();
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
        Invoke(nameof(Spawn), Random.Range(spawnPeriod, spawnPeriod + factor));
        //Invoke(nameof(Spawn), spawnPeriod + 2);
        //Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
        //Invoke(nameof(Spawn), Random.Range(spawnPeriod, spawnPeriod + 1 + GameManager.Instance.difficultyFactor));
        //Debug.Log(spawnPeriod);
    }
}
