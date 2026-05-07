using TMPro;
using UnityEngine;

/// <summary>
/// This class handles methods to control the score.
/// </summary>
public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText; // the text displaying the score

    private void Update()
    {
        UpdateScoreText();
    }

    /// <summary>
    /// Increment the score by 1.
    /// </summary>
    public void AddScore()
    {
        
        Score.Instance.score++;
        Debug.Log("incrementing score, score=" + Score.Instance.score);
    }

    /// <summary>
    /// Decrement the score by 1.
    /// </summary>
    public void DecrementScore()
    {
        Score.Instance.score--;
    }

    /// <summary>
    /// Update what's displayed on the score text.
    /// </summary>
    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + Score.Instance.score.ToString();
    }
}
