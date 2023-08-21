using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMemoryPool : MonoBehaviour
{
    public void CheckIsEnemyClear()
    {
        StartCoroutine("CheckEnemyClearCoroutine");
    }

    public RetVoidParamVoidDelegate OnEnemyClearCallback
    {
        set => onEnemyEmptyCallback = value;
    }

    public GameObject SpawnInit(EEnemyType _enemyType, Vector3 _pos, Transform _parentTr)
    {
        GameObject enemyGo = memoryPools[(int)_enemyType].ActivatePoolItem(5, _parentTr);
        enemyGo.transform.position = _pos;
        enemyGo.GetComponent<EnemyController>().Init(_enemyType);
        enemyGo.GetComponent<EnemyController>().OnDeactivateCallback = DeactivateEnemy;
        return enemyGo;
    }

    public void SetupEnemyMemoryPool(Transform _parentTr)
    {
        for (int i = 0; i < enemyPrefabs.Length; ++i)
        {
            memoryPools[i] = new MemoryPool(enemyPrefabs[i], 5, _parentTr);
        }
    }


    private IEnumerator CheckEnemyClearCoroutine()
    {
        yield return new WaitForSeconds(1f);
        int i = 0;
        while (true)
        {
            for(i = 0; i < memoryPools.Length; ++i)
            {
                if (!memoryPools[i].IsEnableListEmpty())
                    break;
            }

            if(i >= memoryPools.Length)
            {
                onEnemyEmptyCallback?.Invoke();
                Debug.Log("Clear!");
                yield break;
            }

            yield return null;
        }
    }

    private void DeactivateEnemy(EEnemyType _enemyType, GameObject _enemyGo)
    {
        memoryPools[(int)_enemyType].DeactivatePoolItem(_enemyGo);
    }



    private void Awake()
    {
        memoryPools = new MemoryPool[enemyPrefabs.Length];
    }


    [SerializeField]
    private GameObject[] enemyPrefabs = null;

    private MemoryPool[] memoryPools = null;

    private RetVoidParamVoidDelegate onEnemyEmptyCallback = null;
}
