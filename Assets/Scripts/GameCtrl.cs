using H_Utils;
using UnityEngine;

public class GameCtrl : BaseGameCtrl
{
    public static GameCtrl I;
    public UICtrl uiCtrl;

    public GameState CurState;

    private void Awake()
    {
        Application.targetFrameRate = 60;   
        I = this;
    }

    private void Start()
    {
        GameHome();
    }

    private void OnEnable()
    {
        //UIHome
        UIHome.OnClickPlayButton += GameStart;

        //UIGame
        UIGame.OnClickHomeButton += GameHome;
        UIGame.OnClickReplayButton += GameReplay;

        //UILose
        UILose.OnClickReplayButton += GameReplay;
        UILose.OnClickHomeButton += GameHome;
    }

    private void OnDestroy()
    {
        UIHome.OnClickPlayButton -= GameStart;
        //UIGame
        UIGame.OnClickHomeButton -= GameHome;
        UIGame.OnClickReplayButton -= GameReplay;

        //UILose
        UILose.OnClickReplayButton -= GameReplay;
        UILose.OnClickHomeButton -= GameHome;

    }

    public void ChangeState(GameState newState)
    {
        CurState = newState;
    }

    public override void GameHome()
    {
        ChangeState(GameState.None);
        uiCtrl.Show(UIType.Home);
        uiCtrl.Hide(UIType.Game);
    }

    public override void GameStart()
    {
        ChangeState(GameState.Playing);
        uiCtrl.Hide(UIType.Home);
        uiCtrl.Show(UIType.Game);
        ScoreCtrl.I.Inititalize();
        BlockSpawner.I.Initialize();
    }

    public override void GameReplay()
    {
        ChangeState(GameState.Playing);
        BlockSpawner.I.Initialize();
        ScoreCtrl.I.Inititalize();
    }

    public override void GameLose()
    {
        ChangeState(GameState.None);
        uiCtrl.Show(UIType.Lose);
    }
}
