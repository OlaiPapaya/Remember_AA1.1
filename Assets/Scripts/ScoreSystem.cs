using System;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem instance;

    private int currentScore;
    public event Action<int> OnScoreChange;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void AddScore(int score)
    {
        if (score == 0 || (currentScore == 0 && score < 0)) return;

        int previousScore = currentScore;
        currentScore += score;
        if (currentScore < 0)
        {
            currentScore = 0;
            // In order to pass the real score difference to the OnScoreChange action,
            // we cannot rely on the given parameter, as if it contains a negative value
            // that would put currentScore below 0, we clamp it.
            // To fix this, if the score has been clamped, we just pass the difference
            // of score that has actually been applied, and we know that thanks to the
            // score value previous to the change.
            score = -previousScore;
        }
        OnScoreChange?.Invoke(score);
    }

    public int GetScore()
    {
        return currentScore;
    }
}
