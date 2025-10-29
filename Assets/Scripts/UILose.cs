using UnityEngine;
using H_Utils;
using UnityEngine.UI;
using System;
public class UILose : BasePopup
{
    public override UIType Type => UIType.Lose;

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
        _txtScore.text = $"Score: {PrefData.Score.ToString("00")}";
    }

    void UpdateTextBestScore()
    {
       _txtBestScore.text = $"Best Score: {PrefData.BestScore.ToString("00")}";
    }
}
