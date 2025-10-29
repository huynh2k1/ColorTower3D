using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseH;
public class UICtrl : BaseUIManager
{
    public static UICtrl I;
    protected override void Awake()
    {
        base.Awake();
        I = this;
    }

    private void OnEnable()
    {
        HomePanel.OnClickHowToPlayButton += ShowHowToPlay;
    }

    private void OnDisable()
    {
        HomePanel.OnClickHowToPlayButton -= ShowHowToPlay;
    }

    public void ShowHowToPlay()
    {
        Active(PanelType.HowToPlay);
    }
}
