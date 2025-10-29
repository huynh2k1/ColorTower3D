using BaseH;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : PanelBase
{
    [SerializeField] Button _btnTapArea;
    [SerializeField] Button _btnReplay;
    [SerializeField] Button _btnHome;

    public override PanelType Type => PanelType.Game;

    public static Action OnClickHomeButton;
    public static Action OnClickReplayButton;
    public static Action OnClickTapAreaButton;

    private void Awake()
    {
        _btnHome?.onClick.AddListener(RaiseHomeBtnClicked);
        _btnReplay?.onClick.AddListener(RaisePauseBtnClicked);
        _btnTapArea?.onClick.AddListener(RaiseBtnTapArea);
    }

    public void RaiseHomeBtnClicked()
    {
        OnClickHomeButton?.Invoke();
    }

    public void RaisePauseBtnClicked()
    {
        OnClickReplayButton?.Invoke();
    }

    public void RaiseBtnTapArea()
    {
        OnClickTapAreaButton?.Invoke(); 
    }
}
