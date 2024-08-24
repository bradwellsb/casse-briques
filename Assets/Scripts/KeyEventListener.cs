using UnityEngine;

public class KeyEventListener : MonoBehaviour
{
    //get game manager in start or awake
    //if space is pressed and game is not launched, start game
    //if game is launched, check if space is pressed and move paddle
    private GameManager gameManager;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!gameManager.GameStarted) //if game not started, start new game
            {
                gameManager.NewGame();
            }
            else if (gameManager.BallLaunched) //if game started and ball is launched, toggle pause
            {
                gameManager.TogglePause();
            }
            //ball launch is handled in Ball.cs
            //paddle movement is handled in Paddle.cs
        }
        else if (Input.GetKeyDown(KeyCode.Escape)) //escape twice to quit (once if paused)
        {
            if(!gameManager.Paused)
                gameManager.TogglePause();
            else
                Application.Quit();
        }
    }
}
