using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMEnemySpawner : MonoBehaviour
{
    public List<> enemySpawnPoints;
    public GameObject enemyPrefab;


    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemy()
    {
        Instantiate(enemyPrefab);
    }


}
