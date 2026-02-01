using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;

    private float timer = 0f;

    void Start()
    {
        UpdateScoreText();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            score += 1;
            timer -= 1f;
            UpdateScoreText();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
        finalScoreText.text = "Final Score: " + score;
    }
}
