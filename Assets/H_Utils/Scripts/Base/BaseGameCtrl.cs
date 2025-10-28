using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGameCtrl : MonoBehaviour
{
    public virtual void GameHome() { }
    public virtual void GameStart() { }
    public virtual void GameReplay() { }
    public virtual void GameLose() { }
}

public enum GameState
{
    None,
    Playing
}