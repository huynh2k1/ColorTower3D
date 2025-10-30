using System;
using BaseH;
using UnityEngine;
using UnityEngine.UI;

public class UISelectMap : MonoBehaviour
{
    [SerializeField] Button _btnPrev;
    [SerializeField] Button _btnNext;
    [SerializeField] Button _btnTapToPlay;

    public static Action OnPrevClicked;
    public static Action OnNextClicked;
    public static Action OnTapToPlayClicked;

    public void Awake()
    {
        _btnPrev.onClick.AddListener(BtnPrevClicked);
        _btnNext.onClick.AddListener(BtnNextClicked);
        _btnTapToPlay.onClick.AddListener(BtnPlayClicked);
    }

    public void BtnPrevClicked()
    {
        OnPrevClicked?.Invoke();
    }

    public void BtnNextClicked()
    {
        OnNextClicked?.Invoke();
    }

    public void BtnPlayClicked()
    {
        SoundControl.I.PlaySoundByType(SoundType.CLICK);
        Hide();
        OnTapToPlayClicked?.Invoke();   
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);    
    }
}
