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
        DataPlayerPref.Score = _score; 
        UpdateTextScore();
    }

    public void AddScore(int value)
    {
        _score += value;
        DataPlayerPref.Score = _score;
        if(DataPlayerPref.Score > DataPlayerPref.BestScore)
        {
            DataPlayerPref.BestScore = DataPlayerPref.Score;
        }
        UpdateTextScore();
    }   

    public void UpdateTextScore()
    {
        _txtScore.text = DataPlayerPref.Score.ToString();
    }
}
