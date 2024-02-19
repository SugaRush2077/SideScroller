using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_spawner : MonoBehaviour
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
    public float minSpawnRate = 0.1f;
    public float maxSpawnRate = 0.1f;
    //private float spawnPeriod = 1f;
    
      
    private void OnEnable()
    {
        Spawn();
        //Invoke(nameof(Spawn), Random.Range(0, 0.3f));
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
                GameObject background = Instantiate(obj.prefab);
                background.transform.position += transform.position;
                break;
            }

            spawnProbability -= obj.spawnChance;
        }
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
        
    }
}
