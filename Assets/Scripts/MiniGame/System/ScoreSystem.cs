using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public int GetScore()
    {
        return score;
    }
}
