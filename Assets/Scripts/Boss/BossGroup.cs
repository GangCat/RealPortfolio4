using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BossGroup : MonoBehaviour
{
    public void Init(Vector3 _spawnPos, Transform _targetTr)
    {
        targetTr = _targetTr;
        transform.position = _spawnPos;
    }

    public void SpawnFirstBoss()
    {
        arrBossController[0].gameObject.SetActive(true);
        arrBossController[0].Init(targetTr, BirthNextBoss);
        prevBossIdx = 0;
    }

    public void BirthNextBoss()
    {
        int nextBossIdx = prevBossIdx + 1;
        arrBossController[nextBossIdx].SetLocalPosition(arrBossController[prevBossIdx].GetLocalPosition());
        arrBossController[nextBossIdx].SetLocalRotation(arrBossController[prevBossIdx].GetLocalRotation());

        arrBossController[prevBossIdx].gameObject.SetActive(false);
        arrBossController[nextBossIdx].gameObject.SetActive(true);

        arrBossController[nextBossIdx].Init(targetTr, BirthNextBoss);

        prevBossIdx = nextBossIdx;
    }


    [SerializeField]
    private BossController[] arrBossController;

    private int prevBossIdx = 0;

    private Transform targetTr = null;
}
