using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.Rendering;
using UnityEngine;

public class BossManager : MonoBehaviour, IBossEngageObserver
{
    public void Init(Vector3 _bossSpawnPos, int _curLevel, Transform _targetTr)
    {
        listBossGroup = new List<BossGroup>();
        for (int i = 0; i < arrBossGroupPrefab.Length; ++i)
        {
            GameObject bossGroupGo = Instantiate(arrBossGroupPrefab[i], transform);
            listBossGroup.Add(bossGroupGo.GetComponent<BossGroup>());
        }
        listBossGroup[_curLevel].Init(_bossSpawnPos, _targetTr);

        curLevel = _curLevel;

        GameManager.Instance.RegisterBossEngageObserver(GetComponent<IBossEngageObserver>());
    }

    public void ToggleBossEngage()
    {
        listBossGroup[curLevel].SpawnFirstBoss();
    }

    [SerializeField]
    private GameObject[] arrBossGroupPrefab = null;

    private List<BossGroup> listBossGroup = null;

    private int curLevel = 0;
}
