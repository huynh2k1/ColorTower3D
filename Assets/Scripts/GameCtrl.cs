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
        HomePanel.OnClickPlayButton += SelectLevel;

        //UIGame
        GamePanel.OnClickHomeButton += HomeScene;
        GamePanel.OnClickReplayButton += ReplayGame;

        //UILose
        PanelLose.OnClickReplayButton += ReplayGame;
        PanelLose.OnClickHomeButton += HomeScene;

        UISelectMap.OnTapToPlayClicked += PlayGame;
    }

    private void OnDestroy()
    {
        HomePanel.OnClickPlayButton -= SelectLevel;
        //UIGame
        GamePanel.OnClickHomeButton -= HomeScene;
        GamePanel.OnClickReplayButton -= ReplayGame;

        //UILose
        PanelLose.OnClickReplayButton -= ReplayGame;
        PanelLose.OnClickHomeButton -= HomeScene;

        UISelectMap.OnTapToPlayClicked -= PlayGame;
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
        uiCtrl.ShowUISelectMap(false);
    }

    public void SelectLevel()
    {
        ChangeState(GameStatus.None);
        uiCtrl.DeActive(PanelType.Home);
        uiCtrl.Active(PanelType.Game);
        uiCtrl.ShowUISelectMap(true);
        ScoreCtrl.I.Inititalize();
        BlockSpawner.I.Initialize();
    }

    public override void PlayGame()
    {
        ChangeState(GameStatus.Playing);
        BlockSpawner.I.SpawnNextBlock();
    }

    public override void ReplayGame()
    {
        ChangeState(GameStatus.None);
        uiCtrl.ShowUISelectMap(true);
        ScoreCtrl.I.Inititalize();
        BlockSpawner.I.Initialize();
    }

    public override void Defeat()
    {
        ChangeState(GameStatus.None);
        uiCtrl.Active(PanelType.Lose);
    }
}
