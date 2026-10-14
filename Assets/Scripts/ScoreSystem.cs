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
        currentScore += score;
        OnScoreChange?.Invoke(score);
    }

    public int GetScore()
    {
        return currentScore;
    }
}
