using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    // Only ONE GameManager
    public static GameManager Instance {  get; private set; }

    // Game Speed
    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease;
    public float gameSpeed;
    //{ get; private set; }
    // Spawner Parameter
    public float player_gravity;
    public float player_jumpForce;

    // Difficulty
    public int difficulty = 1; // 1++
    public float difficultyFactor = 0f;
    private float difficultyPeriod = 20f; // every x score will increase difficulty

    // UI
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public Button retryButton;

    // Player & Spawner
    private Player player;
    private Spawner spawner;

    // Score Management
    private float score;

    // when enabled (before start)
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    // when disabled
    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
        }
    }

    // when start, find all objects
    private void Start()
    {
        player = FindAnyObjectByType<Player>();
        spawner = FindAnyObjectByType<Spawner>();
        NewGame();
    }

    // Create New Game
    public void NewGame()
    {
        // Clear Obstacle For New Game
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        foreach(var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        // Clear Enemy For New Game
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (var enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }

        // Initialize Game Data
        InitializeData();
        
        enabled = true;

        // Activate UI & GameObjects
        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        
        UpdateHighScore();
    }

    public void GameOver()
    {
        // Stop the game
        gameSpeed = 0f;
        enabled = false;

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        
        UpdateHighScore();

    }

    private void Update()
    {
        // increase speed based on time
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        // increase score
        score += gameSpeed * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");
        increaseDifficulty();
    }

    private void UpdateHighScore()
    {
        float hiscore = PlayerPrefs.GetFloat("hiscore", 0);
        if (score > hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetFloat("hiscore", hiscore);
        }

        highScoreText.text = Mathf.FloorToInt(hiscore).ToString("D5");
    }

    private void increaseDifficulty()
    {
        int prev = difficulty;
        // every "difficultyPeriod" will increase 1 difficulty level
        difficulty = Mathf.FloorToInt(score / difficultyPeriod) + 1;
        if(prev != difficulty) {
            difficultyFactor += 0.1f;
        }
        //Debug.Log(prev);
        if(difficultyFactor > 10 ) { 
            difficultyFactor = 10;
        }
    }

    private void InitializeData()
    {
        score = 0f;
        difficulty = 1;
        difficultyFactor = 0f;
        gameSpeed = initialGameSpeed;
        gameSpeedIncrease = 0.5f;

        player_gravity = player.gravity;
        player_jumpForce = player.jumpForce;
    }
}
