using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, IStageObserver, IPauseObserver
{
    public void SetOnEnemyDeadCallback(RetVoidParamVoidDelegate _onEnemyDeadCallback)
    {
        onEnemyDeadCallback = _onEnemyDeadCallback;
    }

    public void CheckStage(int _curStage)
    {
        curStage = _curStage;
        StartCoroutine("SpawnEnemy");
        enemyMemoryPool.CheckIsEnemyClear();
    }

    public void CheckPaused(bool _isPaused)
    {
        isPaused = _isPaused;
    }

    public void ResetSpawnPos(Vector3 _minSpawnPoint, Vector3 _maxSpawnPoint)
    {
        minSpawnPosition = _minSpawnPoint;
        maxSpawnPosition = _maxSpawnPoint;
    }

    public void InitEnemyClearCallback(RetVoidParamVoidDelegate _enemyClearCallback)
    {
        enemyMemoryPool.OnEnemyClearCallback = _enemyClearCallback;
    }

    private IEnumerator SpawnEnemy()
    {
        int ttlEnemyCnt = enemySpawnCnt + (curStage * 2);

        for (int i = 0; i < ttlEnemyCnt; ++i)
        {
            GameObject enemyGo = enemyMemoryPool.SpawnInit(
                (EEnemyType)(Random.Range(0, (int)EEnemyType.None)),
                GetRandomSpawnPosition(),
                transform
                );
            enemyGo.GetComponent<EnemyController>().Setup(player, onEnemyDeadCallback);

            yield return StartCoroutine("WaitSeconds", spawnDelay);
        }
    }

    private IEnumerator WaitSeconds(float _delayTime)
    {
        float curTime = Time.time;
        while (Time.time - curTime < _delayTime)
        {
            if (isPaused)
                curTime += Time.deltaTime;

            yield return null;
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float x = Random.Range(minSpawnPosition.x, maxSpawnPosition.x);
        float z = Random.Range(minSpawnPosition.z, maxSpawnPosition.z);

        return new Vector3(x, 0.1f, z);
    }

    private void Awake()
    {
        gameManager = GameManager.Instance;
        enemyMemoryPool = GetComponent<EnemyMemoryPool>();
    }

    private void Start()
    {
        gameManager.RegisterStageobserver(GetComponent<IStageObserver>());
        gameManager.RegisterPauseObserver(GetComponent<IPauseObserver>());
        enemyMemoryPool.SetupEnemyMemoryPool(transform);
    }


    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float spawnDelay = 1f;

    private int enemySpawnCnt = 5;
    private int curStage = 0;

    private bool isPaused = false;

    private Vector3 minSpawnPosition = Vector3.zero;
    private Vector3 maxSpawnPosition = Vector3.zero;

    private GameManager         gameManager = null;
    private EnemyMemoryPool     enemyMemoryPool = null;
    private RetVoidParamVoidDelegate onEnemyDeadCallback = null;
}
