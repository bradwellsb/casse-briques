using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rbody;    
    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !FindObjectOfType<GameManager>().Launched)
        {
            SetRandomTrajectory();
            FindObjectOfType<GameManager>().Launched = true;
        }
    }

    private void SetRandomTrajectory() 
    {
        // Randomize direction
        Vector2 direction = new Vector2(Random.Range(-1f, 1f), -1f).normalized;
        rbody.velocity = direction * speed;
    }

    public void Reset()
    {
        transform.position = Vector2.zero;
        rbody.velocity = Vector2.zero;
    }
}
