using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    /// <summary>
    /// ���� ���������� ���� �� �ִ� ���� Ʈ���Ÿ� Ȱ��ȭ
    /// </summary>
    public void ActivateDoorTrigger(int _curStageNum)
    {
        stages[_curStageNum].ActivateDoorTrigger();
    }

    public Vector3 GetMinSpawnPoint(int _curStageNum)
    {
        return stages[_curStageNum].GetMinSpawnPoint();
    }

    public Vector3 GetMaxSpawnPoint(int _curStageNum)
    {
        return stages[_curStageNum].GetMaxSpawnPoint();
    }

    public void Init(int _minRoomCnt, OnPlayerMoveToNextStageDelegate _callback)
    {
        stageGenerator.GenerateLevel(_minRoomCnt, _callback);
    }

    private void Awake()
    {
        stageGenerator = GetComponent<StageGenerator>();
    }

    [SerializeField]
    private GameObject[] stagePrefabs = null;

    private Stage[] stages = null;
    private StageGenerator stageGenerator = null;
}
