using H_Utils;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : BaseUI
{
    [SerializeField] Button _btnTapArea;
    [SerializeField] Button _btnReplay;
    [SerializeField] Button _btnHome;

    public override UIType Type => UIType.Game;

    public static Action OnClickHomeButton;
    public static Action OnClickReplayButton;
    public static Action OnClickTapAreaButton;

    private void Awake()
    {
        _btnHome?.onClick.AddListener(HandleHomeBtnClicked);
        _btnReplay?.onClick.AddListener(HandlePauseBtnClicked);
        _btnTapArea?.onClick.AddListener(HandleBtnTapArea);
    }

    public void HandleHomeBtnClicked()
    {
        OnClickHomeButton?.Invoke();
    }

    public void HandlePauseBtnClicked()
    {
        OnClickReplayButton?.Invoke();
    }

    public void HandleBtnTapArea()
    {
        OnClickTapAreaButton?.Invoke(); 
    }
}
