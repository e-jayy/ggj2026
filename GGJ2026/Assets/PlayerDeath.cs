using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [Header("UI")]
    public GameObject gameOverCanvas;
    public GameObject inGameCanvas;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            // Enable Game Over UI
            gameOverCanvas.SetActive(true);

            // Disable In-Game UI
            inGameCanvas.SetActive(false);

            Time.timeScale = 0f;

            Debug.Log("Player has died");
        }
    }
}
