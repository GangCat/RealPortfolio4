using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GateSystemController : MonoBehaviour
{
    public void Init(RetVoidParamVec3Vec3Delegate _warpPlayerCallback)
    {
        warpPlayerCallback = _warpPlayerCallback;
        gate.Init();
        gateTrigger.Init(MovePlayer, CloseGate);
        gateControlConsole.Init(OpenGate);
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

    public void ActivateGate()
    {
        gateControlConsole.ActivateGate();
    }

    private void OpenGate()
    {
        gate.OpenGate();
    }

    private void CloseGate()
    {
        gate.CloseGate();
    }


    [SerializeField]
    private GateTrigger         gateTrigger = null;
    [SerializeField]
    private GateControlConsole  gateControlConsole = null;
    [SerializeField]
    private Gate                gate = null;
    [SerializeField]
    private EGateDir            gateDir =  EGateDir.None;

    private RetVoidParamVec3Vec3Delegate warpPlayerCallback = null;
}
