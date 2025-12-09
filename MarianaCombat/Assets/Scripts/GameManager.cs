using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TextMeshProUGUI scoreText;
    public int score, numberOfEnemiesPerWave;
    private int currentWaveNumber;
    PlayerScript player;
    public GameObject gameOverText;
    public float gameDelay = 3f;
    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        gameOverText.SetActive(false);
        Instance = this;
        score = 0;
        scoreText.text = "Score: " + score;
        player = FindFirstObjectByType<PlayerScript>();
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
        gameOver = true;
        player.gameObject.SetActive(false);
        gameOverText.SetActive(true);
        StartCoroutine(RestartGameCoroutine());
    }

    IEnumerator RestartGameCoroutine()
    {
        yield return new WaitForSeconds(gameDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
