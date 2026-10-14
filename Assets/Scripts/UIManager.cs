using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    private void OnEnable()
    {
        if (ScoreSystem.instance != null)
            ScoreSystem.instance.OnScoreChange += ScoreChanged;
    }

    private void OnDisable()
    {
        if (ScoreSystem.instance != null)
            ScoreSystem.instance.OnScoreChange -= ScoreChanged;
    }

    private void ScoreChanged(int balance)
    {
        Debug.Log("A");
        if (scoreText == null || ScoreSystem.instance == null) return;
        scoreText.text = ScoreSystem.instance.GetScore().ToString();
    }
}
