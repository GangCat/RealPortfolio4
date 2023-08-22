using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateConsole : InteractiveObjectBase
{
    public void Init(RetVoidParamVoidDelegate _onOpenGateCallback)
    {
        onOpenGateCallback = _onOpenGateCallback;
    }

    public void ActivateGate()
    {
        GetComponent<Collider>().enabled = true;
    }

    public override void Use()
    {
        onOpenGateCallback?.Invoke();
        //gate.OpenGate();
    }

    private void Awake()
    {
        gate = GetComponentInChildren<GateDoor>();
    }

    private void Start()
    {
        GetComponent<Collider>().enabled = false;
    }

    private GateDoor gate = null;
    private RetVoidParamVoidDelegate onOpenGateCallback = null;
}
