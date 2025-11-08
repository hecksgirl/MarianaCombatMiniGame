using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TextMeshProUGUI scoreText;
    public int score, numberOfEnemiesPerWave;
    private int currentWaveNumber;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        score = 0;
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int scoreChange)
    {
        score += scoreChange;
        scoreText.text = "Score: " + score;
        if (score % numberOfEnemiesPerWave == 0)
        { 
            currentWaveNumber++;
            EnemyManager.Instance.SpawnGreatWhite();

        }

    }

    public void GameOver()
    {

    }
}
