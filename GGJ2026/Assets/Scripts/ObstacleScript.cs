using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public float baseSpeed = 3f;
    public float speedIncreasePerScore = 0.02f;

    private Rigidbody2D rb;
    private ScoreManager scoreManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreManager = FindFirstObjectByType<ScoreManager>();
    }

    void Update()
    {
        //Debug.Log("Obstacle speed: " + baseSpeed + (scoreManager.score * speedIncreasePerScore));
    }

    void FixedUpdate()
    {
        float currentSpeed = baseSpeed + (scoreManager.score * speedIncreasePerScore);
        rb.linearVelocity = Vector2.left * currentSpeed;
    }
}
