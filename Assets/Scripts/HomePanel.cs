using BaseH;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HomePanel : PanelBase
{
    [SerializeField] Button _btnPlay;
    [SerializeField] Button _btnHowToPlay;

    public override PanelType Type => PanelType.Home;

    public static Action OnClickPlayButton;
    public static Action OnClickHowToPlayButton;

    private void Awake()
    {
        _btnPlay?.onClick.AddListener(ClickPlay);
        _btnHowToPlay?.onClick.AddListener(ClickHowToPlay);   
    }

    public void ClickPlay()
    {
        OnClickPlayButton?.Invoke();
    }

    public void ClickHowToPlay()
    {
        OnClickHowToPlayButton?.Invoke();
    }
}
