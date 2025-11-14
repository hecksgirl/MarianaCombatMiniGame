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
    PlayerScript player;
    public GameObject gameOverText;
    public float gameDelay = 3f;

    // Start is called before the first frame update
    void Start()
    {
        gameOverText.SetActive(false);
        Instance = this;
        score = 0;
        scoreText.text = "Score: " + score;
        player = FindFirstObjectByType<PlayerScript>();
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
        player.gameObject.SetActive(false);
        gameOverText.SetActive(true);
        StartCoroutine(RestartGameCoroutine());
    }

    IEnumerator RestartGameCoroutine()
    {
        yield return new WaitForSeconds(gameDelay);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
