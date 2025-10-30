using UnityEngine;
using BaseH;
using UnityEngine.UI;
using System;
public class PanelLose : Panel
{
    public override PanelType Type => PanelType.Lose;

    [SerializeField] Button _btnHome;
    [SerializeField] Button _btnReplay;

    public static Action OnClickHomeButton;
    public static Action OnClickReplayButton;

    [SerializeField] Text _txtScore;
    [SerializeField] Text _txtBestScore;

    public override void Awake()
    {
        base.Awake();
        _btnHome?.onClick.AddListener(HomeClick);
        _btnReplay?.onClick.AddListener(ReplayClick);
    }

    public override void Show()
    {
        base.Show();
        UpdateTextScore();
        UpdateTextBestScore();
    }

    void HomeClick()
    {
        Hide();
        OnClickHomeButton?.Invoke();
    }

    void ReplayClick()
    {
        Hide();
        OnClickReplayButton?.Invoke();
    }

    void UpdateTextScore()
    {
        _txtScore.text = $"Score: {DataPlayerPref.Score.ToString("00")}";
    }

    void UpdateTextBestScore()
    {
       _txtBestScore.text = $"Best Score: {DataPlayerPref.BestScore.ToString("00")}";
    }
}
