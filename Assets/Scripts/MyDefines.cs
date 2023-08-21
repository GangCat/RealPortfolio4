public delegate void RetVoidParamVoidDelegate();
public delegate void RetVoidRaramIntDelegate(int _valueInt);
public delegate void RetVoidParamStringDelegate(string _valueStr);
public delegate void RetVoidParamStageStateDelegate(EStageState _valueStageState);

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
