using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const int numLevels = 4;
    private const int numLives = 5;
    private const int startingLevel = 1;
    public static GameManager Instance;
    private Ball ball;
    private Paddle paddle;
    public int score = 0;
    public int lives = numLives;
    public int level = startingLevel;
    public bool BallLaunched = false;
    public bool GameStarted = false;
    public bool Paused = false;
    private void Awake()
    {
        if (Instance == null) // If there is no instance already set
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this) // If there is an instance already set and it's not this one
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        GameStarted = true;
        BallLaunched = false;
        score = 0;
        lives = numLives;
        LoadLevel(startingLevel);
    }
    private void LoadLevel(int levelToLoad)
    {
        level = levelToLoad;
        SceneManager.LoadScene("Level" + levelToLoad);
    }
    public void ResetLevel()
    {
        if(ball is not null && paddle is not null)
        {
            ball.Reset();
            paddle.Reset();
        }
    }
    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        //if scene name contains Level
        if (scene.name.Contains("Level"))
        {
            ball = FindObjectOfType<Ball>();
            paddle = FindObjectOfType<Paddle>();
        }
    }
    public void GameOver()
    {
        GameStarted = false;
        SceneManager.LoadScene("GameOver");
    }
    public void Hit(Brick brick)
    {
        score += brick.points;
        if (IsLevelComplete())
        {
            if (level < numLevels)
                LoadLevel(level + 1);
            else
                Win();
        }
    }
    public void Win()
    {
        GameStarted = false;
        SceneManager.LoadScene("GameWin");
    }
    public void Miss()
    {
        BallLaunched = false;
        lives--;
        if (lives > 0)
        {
            ResetLevel();
        }
        else
        {
            GameOver();
        }
    }

    private void PauseGame()
    {
        if (!Paused)
        {
            Paused = true;
            Time.timeScale = 0f;
        }
    }

    private void ResumeGame()
    {
        if (Paused)
        {
            Paused = false;
            Time.timeScale = 1f;
        }
    }

    public void TogglePause()
    {
        if (Paused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private bool IsLevelComplete()
    {
        Brick[] bricks = FindObjectsOfType<Brick>();
        foreach (Brick brick in bricks)
        {
            if (brick.breakable && brick.gameObject.activeInHierarchy)
            {
                return false;
            }
        }
        return true;
    }
}
