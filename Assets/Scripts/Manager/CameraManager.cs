using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public void WarpCamera(Vector3 _warpDir)
    {
        mainCam.WarpCamera(_warpDir);
    }

    public void Init()
    {
        mainCam = GetComponentInChildren<MainCam>();
    }

    private MainCam mainCam = null;
}
