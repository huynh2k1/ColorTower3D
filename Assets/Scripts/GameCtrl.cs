using BaseH;
using UnityEngine;

public class GameCtrl : GameManagerBase
{
    public static GameCtrl I;
    public UICtrl uiCtrl;

    public GameStatus CurState;

    private void Awake()
    {
        Application.targetFrameRate = 60;   
        I = this;
    }

    private void Start()
    {
        HomeScene();
    }

    private void OnEnable()
    {
        //UIHome
        HomePanel.OnClickPlayButton += PlayGame;

        //UIGame
        GamePanel.OnClickHomeButton += HomeScene;
        GamePanel.OnClickReplayButton += ReplayGame;

        //UILose
        PanelLose.OnClickReplayButton += ReplayGame;
        PanelLose.OnClickHomeButton += HomeScene;
    }

    private void OnDestroy()
    {
        HomePanel.OnClickPlayButton -= PlayGame;
        //UIGame
        GamePanel.OnClickHomeButton -= HomeScene;
        GamePanel.OnClickReplayButton -= ReplayGame;

        //UILose
        PanelLose.OnClickReplayButton -= ReplayGame;
        PanelLose.OnClickHomeButton -= HomeScene;

    }

    public void ChangeState(GameStatus newState)
    {
        CurState = newState;
    }

    public override void HomeScene()
    {
        ChangeState(GameStatus.None);
        uiCtrl.Active(PanelType.Home);
        uiCtrl.DeActive(PanelType.Game);
    }

    public override void PlayGame()
    {
        ChangeState(GameStatus.Playing);
        uiCtrl.DeActive(PanelType.Home);
        uiCtrl.Active(PanelType.Game);
        ScoreCtrl.I.Inititalize();
        BlockSpawner.I.Initialize();
    }

    public override void ReplayGame()
    {
        ChangeState(GameStatus.Playing);
        BlockSpawner.I.Initialize();
        ScoreCtrl.I.Inititalize();
    }

    public override void Defeat()
    {
        ChangeState(GameStatus.None);
        uiCtrl.Active(PanelType.Lose);
    }
}
