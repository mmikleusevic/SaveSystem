using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Zadaca2
{
    public class GameManager : MonoBehaviour
    {
        private const string HIGHSCORE = "HIGHSCORE";
        
        public static GameManager Instance { get; private set; }

        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private TextMeshProUGUI highScoreText;
        [SerializeField] private Player playerPrefab;
        [SerializeField] private Enemy enemyPrefab;
        
        [SerializeField] private Vector3[] enemySpawnPositions;
        
        private int score = 0;
        private int highscore = 0;
        private bool isPlayerAlive = true;

        private Player player;
        
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            highscore = PlayerPrefs.GetInt(HIGHSCORE, 0);
            highScoreText.text = "Highscore: " + highscore;
        }

        public void Play()
        {
            mainMenuPanel.SetActive(false);
            player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            InvokeRepeating(nameof(SpawnEnemy), 2, 3);
        }

        public void IncreaseScore(int amount)
        {
            score += amount;
        }

        private void SpawnEnemy()
        {
            if (!isPlayerAlive) return;
            
            int index = Random.Range(0, enemySpawnPositions.Length);
            
            Vector3 position = enemySpawnPositions[index];
            
            Enemy enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
            enemy.SetTarget(player);
        }

        public void PlayerDied()
        {
            isPlayerAlive = false;
            
            if (score > highscore)
            {
                highscore = score;
                PlayerPrefs.SetInt(HIGHSCORE, highscore);
            }
        }
    }
}