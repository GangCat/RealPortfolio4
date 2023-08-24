using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public void WarpCamera(Vector3 _warpDir)
    {
        mainCam.WarpCamera(_warpDir);
    }

    public void Init(RetVoidParamVoidDelegate _onCamWarpFinishCallback, Transform _playerTr)
    {
        mainCam = GetComponentInChildren<MainCam>();
        mainCam.Init(_onCamWarpFinishCallback, _playerTr);
    }

    private MainCam mainCam = null;
}
