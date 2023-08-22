using UnityEngine;

public delegate void RetVoidParamVoidDelegate();
public delegate void RetVoidRaramIntDelegate(int _valueInt);
public delegate void RetVoidParamStringDelegate(string _valueStr);
public delegate void RetVoidParamStageClassDelegate(Stage _classStage);
public delegate void RetVoidParamVec3Delegate(Vector3 _valueVec3);
public delegate void RetVoidParamVec3Vec3Delegate(Vector3 _lhsValueVec3, Vector3 _rhsValueVec3);

[System.Serializable]
public struct SStagePos
{
    public int x;
    public int y;
    public EStageState stageState;

    public SStagePos(int _x, int _y, EStageState _stageState)
    {
        x = _x;
        y = _y;
        stageState = _stageState;
    }
}

[System.Serializable]
public enum EGateDir { None = -1, Forward, Back, Left, Right }

[System.Serializable]
public enum EStageState { Empty, Start, Normal, Gold, Boss }