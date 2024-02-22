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

    // Player & Spawner
    private Player player;
    private Spawner obstacle_spawner;
    private BG_spawner bg;
    private FG_spawner fg;

    // Game Settings
    // Speed
    public float initialGameSpeed = 5f;
    public float speedIncrease = 0.5f;
    private float gameSpeedIncrease;
    public float gameSpeed;
    // Spawner Parameter
    public float player_gravity;
    public float player_jumpForce;
    // Difficulty
    public int difficulty = 1; // 1++
    public float difficultyFactor = 0f;
    private float difficultyPeriod = 50f; // every x score will increase difficulty
    // Game Mode
    private bool isInGame = false;
    //private bool isDemo = true;
    // Score Management
    private float score;

    // UI: Menu
    public TextMeshProUGUI Title;
    public Button startButton;
    // UI: GameOver
    public TextMeshProUGUI gameOverText;
    public Button retryButton;
    public Button menuButton;
    // UI: Score
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI ScoreTitle;



    private void setDefault()
    {
        score = 0f;
        difficulty = 1;
        difficultyFactor = 0f;
        gameSpeed = initialGameSpeed;
        gameSpeedIncrease = speedIncrease;

        player_gravity = player.gravity;
        player_jumpForce = player.jumpForce;
    }

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
        obstacle_spawner = FindAnyObjectByType<Spawner>();
        bg = FindAnyObjectByType<BG_spawner>();
        fg = FindAnyObjectByType<FG_spawner>();
        GameMode(0);
        setDemo();
        //setDefault();
        //enabled = true;
    }
    private void setDemo()
    {
        score = 0f;
        difficulty = 1;
        difficultyFactor = 0f;
        gameSpeed = initialGameSpeed;
        gameSpeedIncrease = speedIncrease;

        player_gravity = player.gravity;
        player_jumpForce = player.jumpForce;
    }

    private void ClearObject()
    {
        // Clear Obstacle For New Game
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        // Clear Enemy For New Game
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (var enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }

        // Clear background & foreground For New Game
        Background[] backgroundArray = FindObjectsOfType<Background>();
        foreach (var Background in backgroundArray)
        {
            Destroy(Background.gameObject);
        }
        Foreground[] foregroundArray = FindObjectsOfType<Foreground>();
        foreach (var Foreground in foregroundArray)
        {
            Destroy(Foreground.gameObject);
        }
    }
    
    public void Menu()
    {
        enabled = true;
        ClearObject();
        setDemo();
        GameMode(0);
    }

    public void NewGame()
    {
        enabled = true;
        ClearObject();
        setDefault();   // Initialize Game Data
        GameMode(1);
        UpdateHighScore();
    }

    public void GameOver()
    {
        enabled = false;
        GameMode(2);
        UpdateHighScore();
    }

    private void Update()
    {
        if (isInGame)
        {
            // increase speed based on time
            gameSpeed += gameSpeedIncrease * Time.deltaTime;
            // increase score
            score += gameSpeed * Time.deltaTime;
            scoreText.text = Mathf.FloorToInt(score).ToString("D5");
            increaseDifficulty();
        }
        
        
        // esc to quit the game
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
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

    private void GameMode(int index) // 0: menu, 1: in-game 2: game over
    {
        if (index == 0)
        {
            // UI
            Title.gameObject.SetActive(true);
            startButton.gameObject.SetActive(true);
            gameOverText.gameObject.SetActive(false);
            retryButton.gameObject.SetActive(false);
            menuButton.gameObject.SetActive(false);
            // score
            scoreText.gameObject.SetActive(false);
            highScoreText.gameObject.SetActive(false);
            ScoreTitle.gameObject.SetActive(false);

            // Objects
            player.gameObject.SetActive(true);
            obstacle_spawner.gameObject.SetActive(false);
            bg.gameObject.SetActive(true);
            fg.gameObject.SetActive(true);

            //isDemo = true;
            isInGame = false;

        }
        else if (index == 1)
        {
            // UI
            Title.gameObject.SetActive(false);
            startButton.gameObject.SetActive(false);
            gameOverText.gameObject.SetActive(false);
            retryButton.gameObject.SetActive(false);
            menuButton.gameObject.SetActive(false);
            // score
            scoreText.gameObject.SetActive(true);
            highScoreText.gameObject.SetActive(true);
            ScoreTitle.gameObject.SetActive(true);

            // Objects
            player.gameObject.SetActive(true);
            obstacle_spawner.gameObject.SetActive(true);
            bg.gameObject.SetActive(true);
            fg.gameObject.SetActive(true);

            //isDemo = false;
            isInGame = true;

        }
        else if (index == 2)
        {
            // UI
            Title.gameObject.SetActive(false);
            startButton.gameObject.SetActive(false);
            gameOverText.gameObject.SetActive(true);
            retryButton.gameObject.SetActive(true);
            menuButton.gameObject.SetActive(true);
            // score
            scoreText.gameObject.SetActive(true);
            highScoreText.gameObject.SetActive(true);
            ScoreTitle.gameObject.SetActive(true);

            // Objects
            player.gameObject.SetActive(false);
            obstacle_spawner.gameObject.SetActive(false);
            bg.gameObject.SetActive(false);
            fg.gameObject.SetActive(false);

            gameSpeed = 0f;  // Stop the game
            //isDemo = false;
            isInGame = false;
        }
    }

}
