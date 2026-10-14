using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    void Start()
    {
        Debug.Log(ScoreSystem.instance == null);
        if (ScoreSystem.instance != null)
            ScoreSystem.instance.OnScoreChange += ScoreChanged;
    }

    private void OnDestroy()
    {
        if (ScoreSystem.instance != null)
            ScoreSystem.instance.OnScoreChange -= ScoreChanged;
    }

    private void ScoreChanged(int balance)
    {
        if (scoreText == null || ScoreSystem.instance == null) return;
        scoreText.text = ScoreSystem.instance.GetScore().ToString();
    }
}
