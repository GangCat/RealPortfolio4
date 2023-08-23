using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateControlConsole : InteractiveObjectBase
{
    public void Init(RetVoidParamVoidDelegate _onOpenGateCallback)
    {
        onOpenGateCallback = _onOpenGateCallback;
        GetComponent<Collider>().enabled = false;
    }

    public void ActivateGate()
    {
        GetComponent<Collider>().enabled = true;
    }

    public override void Use()
    {
        onOpenGateCallback?.Invoke();
    }

    private RetVoidParamVoidDelegate onOpenGateCallback = null;
}
