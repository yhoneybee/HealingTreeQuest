using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] int miniGameIndex;

    [SerializeField] Text scoreText;

    List<int> firstScore = new List<int>();
    List<int> secondScore = new List<int>();
    List<int> thirdScore = new List<int>();

    int score = 0;

    void Start()
    {
        firstScore.Add(PlayerPrefs.GetInt("0_First"));
        secondScore.Add(PlayerPrefs.GetInt("0_Second"));
        thirdScore.Add(PlayerPrefs.GetInt("0_Third"));

        firstScore.Add(PlayerPrefs.GetInt("1_First"));
        secondScore.Add(PlayerPrefs.GetInt("1_Second"));
        thirdScore.Add(PlayerPrefs.GetInt("1_Third"));

        firstScore.Add(PlayerPrefs.GetInt("2_First"));
        secondScore.Add(PlayerPrefs.GetInt("2_Second"));
        thirdScore.Add(PlayerPrefs.GetInt("2_Third"));

        firstScore.Add(PlayerPrefs.GetInt("3_First"));
        secondScore.Add(PlayerPrefs.GetInt("3_Second"));
        thirdScore.Add(PlayerPrefs.GetInt("3_Third"));

        SetScore(0);
    }

    public void ScorePlus(int score)
    {
        SoundManager.Instance.Play("ScorePlus", SoundType.EFFECT);
        this.score += score;
        SetScoreText();
    }

    public void ScoreMinus(int score)
    {
        SoundManager.Instance.Play("ScoreMinus", SoundType.EFFECT);
        this.score -= score;
        SetScoreText();
    }

    public void SetScore(int score)
    {
        this.score = score;
        SetScoreText();
    }

    public void SetScoreText()
    {
        scoreText.text = score.ToString();
    }

    public int GetScore()
    {
        return score;
    }

    void PushChart(int index, string current, string target)
    {
        PlayerPrefs.SetInt($"{index}_{target}", PlayerPrefs.GetInt($"{index}_{current}"));
    }

    public int[] GetScoreChart(int index)
    {
        int[] scoreChart = new int[3];

        firstScore[index] = PlayerPrefs.GetInt($"{index}_First");
        secondScore[index] = PlayerPrefs.GetInt($"{index}_Second");
        thirdScore[index] = PlayerPrefs.GetInt($"{index}_Third");

        if (score > firstScore[index])
        {
            PushChart(index, "Second", "Third");
            PushChart(index, "First", "Second");
            PlayerPrefs.SetInt($"{index}_First", score);
        }
        else if (score > secondScore[index])
        {
            PushChart(index, "Second", "Third");
            PlayerPrefs.SetInt($"{index}_Second", score);
        }
        else if (score > thirdScore[index])
        {
            PlayerPrefs.SetInt($"{index}_Third", score);
        }

        scoreChart[0] = PlayerPrefs.GetInt($"{index}_First");
        scoreChart[1] = PlayerPrefs.GetInt($"{index}_Second");
        scoreChart[2] = PlayerPrefs.GetInt($"{index}_Third");

        return scoreChart;
    }
}
