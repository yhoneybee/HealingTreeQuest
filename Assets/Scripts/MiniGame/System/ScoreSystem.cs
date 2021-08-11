using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    int score = 0;

    public void ScorePlus(int score)
    {
        this.score += score;
    }

    public void ScoreMinus(int score)
    {
        this.score -= score;
    }

    public void SetScore(int score)
    {
        this.score = score;
    }

    public void SetScoreText(ref Text text)
    {
        text.text = $"Score : {GetScore()}";
    }

    public int GetScore()
    {
        return score;
    }
}
