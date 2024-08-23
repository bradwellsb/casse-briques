using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    private GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    //oncollisionenter2d if ball hits bottom of screen
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == nameof(Ball))
        {
            gameManager.Miss();
        }
    }

}
