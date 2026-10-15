using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject addPointsAnimation;

    void Start()
    {
        if (ScoreSystem.instance != null)
            ScoreSystem.instance.OnScoreChange += ScoreChanged;
    }

    private void OnDestroy()
    {
        if (ScoreSystem.instance != null)
            ScoreSystem.instance.OnScoreChange -= ScoreChanged;
    }

    private void ScoreChanged(int balance, int currentScore)
    {
        if (scoreText == null || balance == 0) return;
        scoreText.text = currentScore.ToString();

        GameObject addPointsObj = Instantiate(addPointsAnimation, transform);
        Destroy(addPointsObj, 1);

        string chosenText = (balance > 0 ? "+" : "") + balance.ToString();
        addPointsObj.transform.GetChild(0).GetComponent<TMP_Text>().text = chosenText;
    }
}
