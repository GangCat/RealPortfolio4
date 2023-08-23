using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public void Init(
    int _minRoomCnt,
    RetVoidParamVec3Vec3Delegate _warpPlayerCallback,
    RetVoidParamStageClassDelegate _stageEnterCallback,
    RetVoidParamVec3Vec3Delegate _enemySpawnPointCallback,
    RetVoidParamVec3Delegate _bossSpawnPointCallback)
    {
        stageGenerator.GenerateLevel(_minRoomCnt, _warpPlayerCallback, _stageEnterCallback, SetListStage);
        enemySpawnPointCallback = _enemySpawnPointCallback;
        bossSpawnPointCallback = _bossSpawnPointCallback;
    }

    public void SetCurStage(Stage _stage)
    {
        curStage = _stage;
    }

    public bool GetIsCurStageClear()
    {
        return curStage.IsClear;
    }

    public EStageState GetCurStageState()
    {
        return curStage.StageState;
    }

    public void ActivateDoorTrigger()
    {
        curStage.ActivateGate();
    }

    public Vector3 GetBossSpawnPos()
    {
        foreach(Stage stage in listStage)
        {
            if (stage.StageState.Equals(EStageState.Boss))
                return stage.GetPosition();
        }

        return Vector3.zero;
    }

    public Vector3 GetCurStageMinSpawnPoint()
    {
        return curStage.GetMinSpawnPoint();
    }

    public Vector3 GetCurStageMaxSpawnPoint()
    {
        return curStage.GetMaxSpawnPoint();
    }

    public void UpdateCurStageClearState(bool _isClear)
    {
        curStage.UpdateClearState(_isClear);
    }

    public void PlayerEnterStage()
    {
        enemySpawnPointCallback?.Invoke(GetCurStageMinSpawnPoint(), GetCurStageMaxSpawnPoint());
    }


    private void Awake()
    {
        stageGenerator = GetComponent<StageGenerator>();
        listStage = new List<Stage>();
    }

    private void SetListStage(Stage[] _arrayStage)
    {
        listStage.AddRange(_arrayStage);
        listStage[0].UpdateClearState(true);
        listStage[0].ActivateGate();
        bossSpawnPointCallback?.Invoke(listStage[listStage.Count - 1].GetPosition());
    }



    [SerializeField]
    private GameObject[] stagePrefabs = null;

    private StageGenerator stageGenerator = null;
    private List<Stage> listStage = null;
    private Stage curStage = null;
    private RetVoidParamVec3Vec3Delegate enemySpawnPointCallback = null;
    private RetVoidParamVec3Delegate bossSpawnPointCallback = null;
}
