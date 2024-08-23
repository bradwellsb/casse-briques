using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public TMP_Text levelText;
    public TMP_Text scoreText;
    public TMP_Text livesText;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene.");
        }
    }

    private void Update()
    {
        if (gameManager != null)
        {
            if (levelText != null)
            {
                levelText.text = $"Level: {gameManager.level}";
            }
            if (scoreText != null)
            {
                scoreText.text = $"Score: {gameManager.score}";
            }
            if (livesText != null)
            {
                livesText.text = $"Lives: {gameManager.lives}";
            }
        }
    }
}
