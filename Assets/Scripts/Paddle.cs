using UnityEngine;

public class Paddle : MonoBehaviour
{
    private Rigidbody2D rbody;
    private Vector2 direction;
    private float speed = 30f;
    private float maxBounceAngle = 75f;
    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            direction = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.zero;
        }
    }
    private void FixedUpdate()
    {
        //if direction not zero, move paddle
        if (direction != Vector2.zero && FindObjectOfType<GameManager>().Launched) //only move paddle if ball has been launched
        {
            rbody.AddForce(direction * speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball != null) //if ball collided with paddle
        {
            var ballRigidbody = ball.GetComponent<Rigidbody2D>();
            float paddleWidth = GetComponent<BoxCollider2D>().size.x;
            Vector3 paddlePosition = transform.position;
            Vector2 contactPoint = collision.GetContact(0).point;
            float offset = paddlePosition.x - contactPoint.x; //distance from center of paddle            

            float angleOfIncidence = Vector2.SignedAngle(Vector2.up, ballRigidbody.velocity);
            float angleOffset = (offset / paddleWidth) * maxBounceAngle; //angle offset based on where ball hit paddle
            float angleOfReflection = Mathf.Clamp(angleOfIncidence + angleOffset, -maxBounceAngle, maxBounceAngle);

            //rotate ball direction based on angle of reflection
            Quaternion rotation = Quaternion.AngleAxis(angleOfReflection, Vector3.forward);
            ballRigidbody.velocity = rotation * Vector2.up * ball.speed; //set ball velocity to new direction
        }
    }

    public void Reset()
    {
        transform.position = new Vector2(0f, transform.position.y);
        rbody.velocity = Vector2.zero;
    }
}
