using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerBase : MonoBehaviour
{
    public virtual void ReplayGame() { }
    public virtual void PlayGame() { }
    public virtual void HomeScene() { }
    public virtual void Defeat() { }
}

public enum GameStatus
{
    None,
    Playing
}