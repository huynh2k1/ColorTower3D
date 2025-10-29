using DG.Tweening;
using System;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public static CameraCtrl I;
    Vector3 initPos;

    private void Awake()
    {
        I = this;
        initPos = transform.position;
    }

    public void Initialize()
    {
        transform.position = initPos;   
    }

    public void MoveToInit(Action actionDone = default)
    {
        transform.DOKill(); 
        transform.DOMove(initPos, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            actionDone?.Invoke();
        });
    }

    public void MoveCamera(int indexSpawn, float blockSize, Action actionDone = default)
    {
        transform.DOKill();
        float targetY = initPos.y + (indexSpawn * blockSize);
        transform.DOMoveY(targetY, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            actionDone?.Invoke();
        });
    }
}
