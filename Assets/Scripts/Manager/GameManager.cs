using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IPauseSubject, IBossEngageSubject
{
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject gameManager = new GameObject("GameManager");
                    instance = gameManager.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    #region PauseObserver
    public void RegisterPauseObserver(IPauseObserver _observer)
    {
        pauseObserverList.Add(_observer);
    }

    public void RemovePauseObserver(IPauseObserver _observer)
    {
        pauseObserverList.Remove(_observer);
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        foreach (IPauseObserver observer in pauseObserverList)
            observer.CheckPause(isPaused);

        if (isPaused)
        {
            canvasPauseMenu.ShowPauseMenu();
        }

        else
        {
            StopCoroutine("ShowElapsedTime");
            canvasPauseMenu.HidePauseMenu();
        }
    }
    #endregion

    #region BossEngageRegion
    public void RegisterBossEngageObserver(IBossEngageObserver _observer)
    {
        bossEngageObserverList.Add(_observer);
    }

    public void RemoveBossEngageObserver(IBossEngageObserver _observer)
    {
        bossEngageObserverList.Remove(_observer);
    }

    public void ToggleBossEngage()
    {
        isBossEngage = !isBossEngage;
        foreach (IBossEngageObserver observer in bossEngageObserverList)
            observer.CheckBossEngage(isBossEngage);
    }
    #endregion

    public void StageStart(Stage _stage)
    {
        stageMng.SetCurStage(_stage);

        if (stageMng.GetIsCurStageClear()) StageClear();

        if (stageMng.GetCurStageState().Equals(EStageState.Normal))
        {
            stageMng.PlayerEnterStage();
            // 맵 들어가고 일시정지 하기
            return;
        }
        else if (stageMng.GetCurStageState().Equals(EStageState.Boss))
        {
            ToggleBossEngage();
            // 맵 들어가고 일시정지 하기
            return;
        }

        StageClear();
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if(mainMenuMng != null)
            mainMenuMng.OnButtonMainCallback = ChangeScene;

        if(playerMng != null)
            playerMng.Init(
                UpdateTotalUsedAmmoCount,
                UpdateTotalGoldPlayerGain,
                UpdateTotalAttackDamage,
                UpdateTotalDamagePlayerTaken
                );

        if (enemyMng != null)
            enemyMng.Init(UpdateTotalEnemyKillCount, StageClear);

        if(canvasPauseMenu != null)
            canvasPauseMenu.Init(TogglePause, ChangeScene);

        if(stageMng != null)
            stageMng.Init(7, MovePlayer, StageStart, enemyMng.PlayerEnterStage);

        if (cameraMng != null)
            cameraMng.Init();

    }

    private void UpdateTotalUsedAmmoCount()
    {
        canvasPauseMenu.UpdateTotalUsedAmmoCount();
    }

    private void UpdateTotalEnemyKillCount()
    {
        canvasPauseMenu.UpdateTotalEnemyKillCount();
    }

    private void UpdateTotalGoldPlayerGain(int _increasedGold)
    {
        canvasPauseMenu.UpdateTotalGoldPlayerGain(_increasedGold);
    }

    private void UpdateTotalDamagePlayerTaken()
    {
        canvasPauseMenu.UpdateTotalDamagePlayerTaken();
    }

    private void UpdateTotalAttackDamage(int _dmg)
    {
        canvasPauseMenu.UpdateTotalAttackDamage(_dmg);
    }

    private void ChangeScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

    private void StageClear()
    {
        stageMng.UpdateCurStageClearState(true);
        stageMng.ActivateDoorTrigger();
    }

    private void MovePlayer(Vector3 _warpPlayerDir,Vector3 _warpCameraDir)
    {
        playerMng.MovePlayerToNextStage(_warpPlayerDir);
        cameraMng.WarpCamera(_warpCameraDir);
        Debug.Log("playerMng");
    }

    private GameManager() { }

    [Header("-Main Menu Manager")]
    [SerializeField]
    private MainMenuUIManager   mainMenuMng = null;

    [Header("-Game Managers")]
    [SerializeField]
    private CanvasPauseMenu     canvasPauseMenu = null;
    [SerializeField]
    private PlayerInputManager  playerMng = null;
    [SerializeField]
    private EnemyManager        enemyMng = null;
    [SerializeField]
    private StageManager        stageMng = null;
    [SerializeField]
    private CrystalManager      crystalMng = null;
    [SerializeField]
    private CameraManager       cameraMng = null; 
    

    private bool isPaused = false;
    private bool isBossEngage = false;

    private static GameManager instance = null;

    private List<IPauseObserver> pauseObserverList = new List<IPauseObserver>();
    private List<IBossEngageObserver> bossEngageObserverList = new List<IBossEngageObserver>();
}
