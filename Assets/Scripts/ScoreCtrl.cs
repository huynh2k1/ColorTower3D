using UnityEngine;
using UnityEngine.UI;

public class ScoreCtrl : MonoBehaviour
{
    public static ScoreCtrl I;
    [SerializeField] Text _txtScore;


    private void Awake()
    {
        I = this;
    }

    public void Inititalize()
    {
        PrefData.Score = 0; 
        UpdateTextScore();
    }

    public void AddScore(int value)
    {
        PrefData.Score += value;
        if(PrefData.Score > PrefData.BestScore)
        {
            PrefData.BestScore = PrefData.Score;
        }
        UpdateTextScore();
    }   

    public void UpdateTextScore()
    {
        _txtScore.text = PrefData.Score.ToString("00");
    }
}
