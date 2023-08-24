using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public void Init(
    int _minRoomCnt,
    RetVoidParamVec3Delegate _warpPlayerCallback,
    RetVoidParamVoidDelegate _stageEnterCallback,
    RetVoidParamVec3Vec3Delegate _enemySpawnPointCallback,
    RetVoidParamVec3Delegate _bossSpawnPointCallback)
    {
        stageGenerator.GenerateLevel(_minRoomCnt, FindNextStage, SetListStage);
        enemySpawnPointCallback = _enemySpawnPointCallback;
        bossSpawnPointCallback = _bossSpawnPointCallback;
        stageEnterCallback = _stageEnterCallback;
        warpPlayerCallback = _warpPlayerCallback;
    }

    public void CamWarpFinish()
    {
        prevStage.SetActive(false);
    }

    private void FindNextStage(Vector3 _warpPlayerDir)
    {
        int tempPosX = curStage.GetX;
        int tempPosY = curStage.GetY;
        if(_warpPlayerDir.x == 0)
        {
            if (_warpPlayerDir.z.Equals(1))
                ++tempPosY;
            else
                --tempPosY;
        }
        else
        {
            if (_warpPlayerDir.x.Equals(1))
                ++tempPosX;
            else
                --tempPosX;
        }

        foreach(Stage stage in listStage)
        {
            if(stage.GetX.Equals(tempPosX) && stage.GetY.Equals(tempPosY))
            {
                prevStage = curStage;
                curStage = stage;
                curStage.SetActive(true);
            }    
        }

        warpPlayerCallback?.Invoke(_warpPlayerDir);
        stageEnterCallback?.Invoke();
    }

    private void SetCurStage(Stage _stage)
    {
        if(curStage != null)
            prevStage = curStage;

        curStage = _stage;
        curStage.SetActive(true);
    }

    public int GetCurStagePosX => curStage.GetX;
    public int GetCurStagePosY => curStage.GetY;

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

        //for (int i = 1; i < listStage.Count; ++i)
        //    listStage[i].SetActive(false);

        SetCurStage(listStage[0]);

        bossSpawnPointCallback?.Invoke(listStage[listStage.Count - 1].GetPosition());
    }



    [SerializeField]
    private GameObject[] stagePrefabs = null;

    private StageGenerator stageGenerator = null;
    private List<Stage> listStage = null;
    private Stage curStage = null;
    private Stage prevStage = null;
    private RetVoidParamVec3Vec3Delegate enemySpawnPointCallback = null;
    private RetVoidParamVec3Delegate bossSpawnPointCallback = null;
    private RetVoidParamVoidDelegate stageEnterCallback = null;
    private RetVoidParamVec3Delegate warpPlayerCallback = null;
}
