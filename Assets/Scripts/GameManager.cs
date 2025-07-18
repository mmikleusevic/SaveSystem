using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private const string HIGHSCORE = "HIGHSCORE";
    
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private GameObject resetPanel;
    [SerializeField] private Player playerPrefab;
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private int score;
    private int highScore;
    private float timer;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //StartGame();
        //InvokeRepeating(nameof(SpawnEnemy), 1f, 3f);
        score = 0;
        highScore = PlayerPrefs.GetInt(HIGHSCORE);
        scoreText.text = "Score: " + score;
        highScoreText.text = "HighScore: " + highScore;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 10)
        {
            CheckIsHighScore();
        }
    }

    private void StartGame()
    {
        Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, Vector3.forward * 7, Quaternion.identity);
    }

    public void ShowResetPanel()
    {
        resetPanel.SetActive(true);
    }

    private void HideResetPanel()
    {
        resetPanel.SetActive(false);
    }

    public void ResetGame()
    {
        Time.timeScale = 1;
        
        HideResetPanel();
        DestroyPlayerAndEnemies();
        StartGame();
    }

    private void DestroyPlayerAndEnemies()
    {
        Player[] players = FindObjectsByType<Player>(FindObjectsSortMode.None);

        foreach (var player in players)
        {
            Destroy(player.gameObject);
        }
        
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        foreach (var enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
    }

    public void HealthUI(float health, float maxHealth)
    {
        float value = health / maxHealth;
        healthBar.fillAmount = value;
        healthText.text = "Health: " + (value * 100) + "%";
    }
    
    public void IncreaseScore(int scoreAmount)
    {
        score += scoreAmount;

        scoreText.text = "Score: " + score;
    }

    private void CheckIsHighScore()
    {
        if (score <= highScore) return;
        
        highScore = score;
        highScoreText.text = "HighScore: " + highScore;
        
        PlayerPrefs.SetInt(HIGHSCORE, highScore);
    }
}
