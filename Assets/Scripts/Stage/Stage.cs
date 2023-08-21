using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public int GetX => stagePos.x;
    public int GetY => stagePos.y;
    public EStageState GetStageState => stagePos.stageState;
    public bool IsClear => isClear;

    public void ActivateDoorTrigger()
    {
        foreach (DoorToNextStage door in doors)
            door.ActivateDoorTrigger();
    }

    public Vector3 GetMinSpawnPoint()
    {
        return minSpawnPoint.GetPosition();
    }

    public Vector3 GetMaxSpawnPoint()
    {
        return maxSpawnPoint.GetPosition();
    }

    public void Init(
        int _x,
        int _y,
        EStageState _stageState,
        RetVoidParamVoidDelegate _moveStageCallback,
        RetVoidParamStageStateDelegate _stageEnterCallback)
    {
        doors = GetComponentsInChildren<DoorToNextStage>();

        foreach (DoorToNextStage door in doors)
            door.Init(_moveStageCallback);

        stagePos.x = _x;
        stagePos.y = _y;
        stagePos.stageState = _stageState;
        stageEnterCallback = _stageEnterCallback;

    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            if (isClear) return;

            stageEnterCallback?.Invoke(stagePos.stageState);
        }
    }

    private DoorToNextStage[] doors = null;

    [SerializeField]
    private MinSpawnPoint minSpawnPoint = null;
    [SerializeField]
    private MaxSpawnPoint maxSpawnPoint = null;

    private bool isClear = false;

    [SerializeField]
    private SStagePos stagePos;

    private RetVoidParamStageStateDelegate stageEnterCallback = null;
}