using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;
namespace BaseH
{
    public class Panel : PanelBase
    {
        public override PanelType Type => throw new NotImplementedException();

        [SerializeField] Button _btnClose;
        [SerializeField] protected Image mask;
        [SerializeField] protected CanvasGroup mainGroup;
        [SerializeField] protected GameObject main;

        [SerializeField] float targetMask = 0.9f;
        [SerializeField] float timeTween = 0.4f;
        [SerializeField] Ease typeTweenShow = Ease.OutBack;
        [SerializeField] Ease typeTweenHide = Ease.InBack;


        public virtual void Awake()
        {
            Initialize();
            _btnClose?.onClick.AddListener(OnClickHide);
        }

        public virtual void OnClickHide()
        {
            Hide();
        }

        public virtual void Initialize()
        {
            mask.raycastTarget = false;

            Color color = mask.color;
            color.a = 0;
            mask.color = color;

            main.SetActive(false);
            mainGroup.blocksRaycasts = false;
        }

        public override void Show()
        {
            ShowMask(true);
            ShowMain();
        }

        public override void Hide()
        {
            ShowMask(false);
            HideMain();
        }

        public virtual void ShowMain(Action actionDone = default)
        {
            main.transform.DOKill();

            mainGroup.blocksRaycasts = false;
            //main.transform.localScale = Vector3.zero;

            main.transform.DOScale(Vector3.one, timeTween).From(0.6f).SetEase(typeTweenShow).OnComplete(() =>
            {
                mainGroup.blocksRaycasts = true;
                actionDone?.Invoke();
            });
            main.SetActive(true);
        }

        public virtual void HideMain(Action actionDone = default)
        {
            main.transform.DOKill();
            mainGroup.blocksRaycasts = false;
            main.transform.localScale = Vector3.one;

            main.transform.DOScale(Vector3.one * 0.4f, timeTween).From(1f).SetEase(typeTweenHide).OnComplete(() =>
            {
                main.SetActive(false);
                actionDone?.Invoke();
            });
        }

        public void ShowMask(bool isShow)
        {
            mask.DOKill();
            Color color = mask.color;   
            if (isShow)
            {
                color.a = 0;
                mask.color = color;
                mask.raycastTarget = true;
                mask.DOFade(targetMask, timeTween).SetEase(Ease.Linear);
            }
            else
            {
                mask.DOFade(0f, timeTween).SetEase(Ease.Linear).OnComplete(() => mask.raycastTarget = false);
            }
        }
    }
}
