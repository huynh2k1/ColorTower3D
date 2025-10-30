using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseH;
public class UICtrl : BaseUIManager
{
    public static UICtrl I;
    public UISelectMap uiSelectMap;
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

    public void ShowUISelectMap(bool isShow)
    {
        if (isShow)
        {
            uiSelectMap.Show();
        }
        else
        {
            uiSelectMap.Hide();
        }
    }
}
