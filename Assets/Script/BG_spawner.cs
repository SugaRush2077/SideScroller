using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BG_spawner : MonoBehaviour
{
    public bool isSpawnTutor = false;
    public GameObject tutorialSign;
    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float spawnChance;
    }

    public SpawnableObject[] objects;
    //private int count = 0;
    // Spawn time in second
    // Depend on distance & speed & time
    public int SpawnNumPerSec = 3;
    public float minSpawnRate = 0.5f;
    public float maxSpawnRate = 1.2f;
    //private float spawnPeriod = 1f;
    
      
    private void OnEnable()
    {
        //Spawn();
        //InvokeRepeating(nameof(Spawn), 1, 0.7f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Update()
    {
        /*
        if(count > (1 / Time.deltaTime) / 10)
        {
            Spawn();
            count = 0;
        }
        
        count++;*/
    }
    public void startSpawn(float n)
    {
        //Debug.Log(n);
        Invoke(nameof(Spawn), n);
    }

    public void spawnTutorialSign()
    {
        GameObject tutorial = Instantiate(tutorialSign);
        tutorial.transform.position += transform.position;
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
        //float min = Time.deltaTime * minSpawnRate;
        //float max = Time.deltaTime * maxSpawnRate;

        float rate = 1f / (SpawnNumPerSec + GameManager.Instance.difficulty);
        //float rate = 1f / 4;
        //Debug.Log(rate);
        float spawnPeriod = Random.Range(rate, Mathf.Min(rate + 1, rate * 10));
        //Debug.Log(spawnPeriod);
        Invoke(nameof(Spawn), spawnPeriod);
    }
}
