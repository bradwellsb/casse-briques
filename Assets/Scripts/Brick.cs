using System.Runtime.CompilerServices;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

public class Brick : MonoBehaviour
{
    private int health;
    private SpriteRenderer spriteRenderer;
    public Sprite[] states;
    public bool breakable = true;
    public int points = 100;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        if(breakable)
        {
            health = states.Length;
            spriteRenderer.sprite = states[health - 1];
        }
    }
    private void Hit()
    {
        if (breakable)
        {
            health--;
            if (health > 0)
            {
                spriteRenderer.sprite = states[health - 1];
            }
            else
            {
                gameObject.SetActive(false);
            }
            FindObjectOfType<GameManager>().Hit(this);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == nameof(Ball))
        {
            Hit();
        }
    }
}
