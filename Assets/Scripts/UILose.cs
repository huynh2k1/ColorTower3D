using UnityEngine;
using BaseH;
using UnityEngine.UI;
using System;
public class UILose : Panel
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
        _btnHome?.onClick.AddListener(HandleHomeBtnClicked);
        _btnReplay?.onClick.AddListener(HandleReplayClicked);
    }

    public override void Show()
    {
        base.Show();
        UpdateTextScore();
        UpdateTextBestScore();
    }

    void HandleHomeBtnClicked()
    {
        Hide();
        OnClickHomeButton?.Invoke();
    }

    void HandleReplayClicked()
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
