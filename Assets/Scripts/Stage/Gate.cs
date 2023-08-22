using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public void Init(RetVoidParamVec3Vec3Delegate _warpPlayerCallback)
    {
        warpPlayerCallback = _warpPlayerCallback;
        trigger.Init(MovePlayer);
        console.Init(OpenGate);
    }

    private void MovePlayer()
    {
        warpPlayerCallback?.Invoke(GetPlayerWarpDir(), GetCameraWarpDir());
    }

    private Vector3 GetPlayerWarpDir()
    {
        if (gateDir.Equals(EGateDir.Forward))
            return Vector3.forward;
        if (gateDir.Equals(EGateDir.Back))
            return Vector3.back;
        if (gateDir.Equals(EGateDir.Left))
            return Vector3.left;
        if (gateDir.Equals(EGateDir.Right))
            return Vector3.right;

        return Vector3.zero;
    }

    private Vector3 GetCameraWarpDir()
    {
        if (gateDir.Equals(EGateDir.Forward))
            return Vector3.forward;
        if (gateDir.Equals(EGateDir.Back))
            return Vector3.back;
        if (gateDir.Equals(EGateDir.Left))
            return Vector3.left;
        if (gateDir.Equals(EGateDir.Right))
            return Vector3.right;

        return Vector3.zero;
    }

    private void OpenGate()
    {
        gate.OpenGate();
    }

    public void ActivateGate()
    {
        console.ActivateGate();
    }

    [SerializeField]
    private GateTrigger trigger = null;
    [SerializeField]
    private GateConsole console = null;
    [SerializeField]
    private GateDoor    gate = null;
    [SerializeField]
    private EGateDir    gateDir =  EGateDir.None;

    private RetVoidParamVec3Vec3Delegate warpPlayerCallback = null;
}
