using UnityEngine;
using UnityEngine.UI;

public class ScoreCtrl : MonoBehaviour
{
    public static ScoreCtrl I;
    [SerializeField] Text _txtScore;
    int _score = 0;

    private void Awake()
    {
        I = this;
    }

    public void Inititalize()
    {
        _score = 0;
        PrefData.Score = _score; 
        UpdateTextScore();
    }

    public void AddScore(int value)
    {
        _score += value;
        PrefData.Score = _score;
        if(PrefData.Score > PrefData.BestScore)
        {
            PrefData.BestScore = PrefData.Score;
        }
        UpdateTextScore();
    }   

    public void UpdateTextScore()
    {
        _txtScore.text = PrefData.Score.ToString();
    }
}
