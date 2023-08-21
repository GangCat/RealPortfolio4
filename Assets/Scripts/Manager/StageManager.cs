using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    /// <summary>
    /// 다음 스테이지로 향할 수 있는 문의 트리거를 활성화
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

    public void Init(int _minRoomCnt, RetVoidParamVoidDelegate _callback)
    {
        stageGenerator.Init(SetListStage);
        stageGenerator.GenerateLevel(_minRoomCnt, _callback);
    }

    private void Awake()
    {
        stageGenerator = GetComponent<StageGenerator>();
        listStage = new List<Stage>();
    }

    private void SetListStage(Stage[] _arrayStage)
    {
        listStage.AddRange(_arrayStage);
    }

    [SerializeField]
    private GameObject[] stagePrefabs = null;

    private Stage[] stages = null;
    private StageGenerator stageGenerator = null;
    private List<Stage> listStage = null;
}
