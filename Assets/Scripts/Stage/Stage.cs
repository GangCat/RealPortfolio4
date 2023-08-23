using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public int GetX => stagePos.x;
    public int GetY => stagePos.y;
    public EStageState StageState => stagePos.stageState;
    public bool IsClear => isClear;

    public void ActivateGate()
    {
        foreach (GateSystemController door in doors)
            door.ActivateGate();
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void UpdateClearState(bool _isClear)
    {
        isClear = _isClear;
    }

    public Vector3 GetPlayerSpawnPoint()
    {
        return Vector3.zero;
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
        RetVoidParamVec3Vec3Delegate _warpPlayerCallback,
        RetVoidParamStageClassDelegate _stageEnterCallback)
    {
        doors = GetComponentsInChildren<GateSystemController>();

        foreach (GateSystemController door in doors)
            door.Init(_warpPlayerCallback);

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

            stageEnterCallback?.Invoke(this);
        }
    }

    private GateSystemController[] doors = null;

    [SerializeField]
    private MinSpawnPoint minSpawnPoint = null;
    [SerializeField]
    private MaxSpawnPoint maxSpawnPoint = null;

    [SerializeField]
    private bool isClear = false;

    [SerializeField]
    private SStagePos stagePos;

    private RetVoidParamStageClassDelegate stageEnterCallback = null;
}