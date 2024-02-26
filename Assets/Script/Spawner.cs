using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObstacle
    {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float spawnChance;
    }
    public struct SpawnableEnemy
    {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float spawnChance;
    }
    public float threshold = 0.5f;

    public SpawnableObstacle[] Obstacle_list;
    public SpawnableObstacle[] Enemy_list;
    public bool DifficultyOn = false;
    // Spawn time in second
    // Depend on distance & speed & time
    public float minSpawnRate = 1f;
    public float maxSpawnRate = 1.5f;
    public float spawnConstant = 0.5f;

    private float spawnPeriod = 1f;
    private int jumpDistance;
    private float jumpForce;
    private float gravity;
    private float difficultyFactor;
    

    // Calculate the distance of player can jump
    private void calculateDistance()
    {
        // Get Player Jump Angle
        // tan = jumpForce / groundSpeed (gameSpeed)
        // angle = 180 / (pi * radian)
        jumpForce = GameManager.Instance.player_jumpForce;
        gravity = GameManager.Instance.player_gravity;
        difficultyFactor = GameManager.Instance.difficultyFactor;
        float gs = GameManager.Instance.gameSpeed;
        float angle = Mathf.Atan(jumpForce / gs); // Radians
        float angle_in_degree = angle * 180 / Mathf.PI;
        // distance = v0 * v0 * Mathf.Sin(2 * angle)) / gravity
        float v0 = Mathf.Sqrt(jumpForce * jumpForce + gravity * gravity);
        
        jumpDistance = Mathf.CeilToInt((v0 * v0 * Mathf.Sin(2 * angle)) / gravity); // plus 1 for boundary
        spawnPeriod = jumpDistance / gs;
        /*
        Debug.Log(jumpForce);
        Debug.Log(gs);
        Debug.Log(angle_in_degree);
        Debug.Log(v0);
        Debug.Log(jumpDistance);
        Debug.Log(spawnPeriod);
        */
        Invoke(nameof(calculateDistance), 0.5f);
    }
    
    private void OnEnable()
    {
        
        //Invoke(nameof(calculateDistance), 0.1f);
        
        
    }

    public void startSpawn(float a, float b)
    {
        Invoke(nameof(Spawn), Random.Range(a, b));
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Update()
    {
        calculateDistance();
    }

    private void Spawn()
    {
        float obstacle_or_enemy = Random.value;
        float spawnProbability = Random.value;
        if(obstacle_or_enemy < threshold) // Obstacle
        {
            foreach (var obj in Obstacle_list)
            {
                if (spawnProbability < obj.spawnChance)
                {
                    GameObject obstacle = Instantiate(obj.prefab);
                    obstacle.transform.position += transform.position;
                    break;
                }

                spawnProbability -= obj.spawnChance;
            }
        }
        else // Enemy
        {
            foreach (var obj in Enemy_list)
            {
                if (spawnProbability < obj.spawnChance)
                {
                    GameObject enemy = Instantiate(obj.prefab);
                    enemy.transform.position += transform.position;
                    break;
                }

                spawnProbability -= obj.spawnChance;
            }
        }

        if (DifficultyOn)
        {
            float minRange = spawnPeriod - difficultyFactor;
            float maxRange = spawnPeriod + spawnConstant;
            //Invoke(nameof(Spawn), Random.Range(minRange, maxRange));
            Invoke(nameof(Spawn), Random.Range(spawnPeriod, spawnPeriod + spawnConstant));
        }
        else
        {
            Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
        }
    }
}
