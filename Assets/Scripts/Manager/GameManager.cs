using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IPauseSubject, IBossEngageSubject, IStageSubject
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
            observer.CheckPaused(isPaused);

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

    #region StageObserver
    public void RegisterStageobserver(IStageObserver _observer)
    {
        stageObserverList.Add(_observer);
    }

    public void RemoveStageObserver(IStageObserver _observer)
    {
        stageObserverList.Remove(_observer);
    }

    public void StageStart()
    {
        ++curStage;
        ResetEnemySpawnPos();

        foreach (IStageObserver observer in stageObserverList)
            observer.CheckStage(curStage);

    }
    #endregion

    private void Awake()
    {
        canvasPauseMenu = FindAnyObjectByType<CanvasPauseMenu>();
        playerMng = FindAnyObjectByType<PlayerInputManager>();
        enemyMng = FindAnyObjectByType<EnemyManager>();
        stageMng = FindAnyObjectByType<StageManager>();
        instance = this;
    }

    private void Start()
    {
        if(mainMenuMng != null)
            mainMenuMng.OnButtonMainCallback = ChangeScene;

        if(playerMng != null)
        {
            playerMng.Init(
                UpdateUsedAmmo,
                UpdateGold,
                UpdateEnemyDamaged,
                UpdateDamagedCount
                );
        }

        if (enemyMng != null)
        {
            enemyMng.SetOnEnemyDeadCallback(CalcDeadEnemy);
            enemyMng.InitEnemyClearCallback(StageClear);
        }

        if(canvasPauseMenu != null)
        {
            canvasPauseMenu.Init(TogglePause, ChangeScene, ChangeScene);
            canvasPauseMenu.UpdateTime();
        }

        if(stageMng != null)
        {
            stageMng.Init(7, StageStart);
        }

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            GameStart();
        }
    }

    public void GameStart()
    {
        ResetEnemySpawnPos();
        enemyMng.CheckStage(curStage);
    }

    private void ResetEnemySpawnPos()
    {
        enemyMng.ResetSpawnPos(stageMng.GetMinSpawnPoint(curStage), stageMng.GetMaxSpawnPoint(curStage));
    }

    private void UpdateUsedAmmo()
    {
        canvasPauseMenu.UpdateTotalUsedAmmoCount();
    }

    private void CalcDeadEnemy()
    {
        canvasPauseMenu.UpdateTotalEnemyKillCount();
    }

    private void UpdateGold(int _increasedGold)
    {
        canvasPauseMenu.UpdateTotalGoldPlayerGain(_increasedGold);
    }

    private void UpdateDamagedCount()
    {
        canvasPauseMenu.UpdateTotalDamagePlayerTaken();
    }

    private void UpdateEnemyDamaged(int _dmg)
    {
        canvasPauseMenu.UpdateTotalAttackDamage(_dmg);
    }

    private void ChangeScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

    private void StageClear()
    {
        stageMng.ActivateDoorTrigger(curStage);
    }

    private GameManager() { }

    [SerializeField]
    private CanvasPauseMenu canvasPauseMenu = null;
    [SerializeField]
    private PlayerInputManager playerMng = null;
    [SerializeField]
    private EnemyManager enemyMng = null;
    [SerializeField]
    private MainMenuUIManager mainMenuMng = null;
    [SerializeField]
    private StageManager stageMng = null;
    [SerializeField]
    private CrystalManager crystalMng = null;
    

    private bool isPaused = false;
    private bool isBossEngage = false;

    private int curStage = 0;

    private static GameManager instance = null;

    private List<IPauseObserver> pauseObserverList = new List<IPauseObserver>();
    private List<IBossEngageObserver> bossEngageObserverList = new List<IBossEngageObserver>();
    private List<IStageObserver> stageObserverList = new List<IStageObserver>();


}
