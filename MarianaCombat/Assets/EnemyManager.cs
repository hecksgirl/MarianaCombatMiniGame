using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    public List<EnemyScript> enemies;
    public GameObject enemyPrefab, enemyBubbleExplode;
    public SpawnPoint[] spawnPoints;
    public int spawnWaitTime;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        enemies = new List<EnemyScript>();
        spawnPoints = FindObjectsByType<SpawnPoint>(FindObjectsSortMode.None);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemy(GameObject enemy)
    {
        StartCoroutine(SpawnEnemyCoroutine(enemy));
    }

    IEnumerator SpawnEnemyCoroutine(GameObject e)
    {
        GameObject ps = Instantiate(enemyBubbleExplode, e.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(spawnWaitTime);
        int randomIndex = Random.Range(0, spawnPoints.Length-1);
        Instantiate(enemyPrefab, spawnPoints[randomIndex].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        Destroy(ps);
    }
}
