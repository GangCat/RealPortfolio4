using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCam : MonoBehaviour
{
    public void Init(RetVoidParamVoidDelegate _onCamWarpFinishCallback, Transform _playerTr)
    {
        onCamWarpFinish = _onCamWarpFinishCallback;
        playerTr = _playerTr;
        StartCoroutine("FollowPlayerCoroutine");
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private IEnumerator FollowPlayerCoroutine()
    {
        while (true)
        {
            
            transform.position = playerTr.position + cameraOffset;

            yield return null;
        }
    }

    public void WarpCamera(Vector3 _warpDir)
    {

        StopCoroutine("FollowPlayerCoroutine");
        StartCoroutine("WarpCameraCoroutine");
    }

    private IEnumerator WarpCameraCoroutine()
    {
        float curTime = Time.time;
        float percent = 0f;
        Vector3 oriPos = transform.position;
        while(percent < 1f)
        {
            percent = (Time.time - curTime) / 0.2f;
            transform.position = Vector3.Slerp(oriPos, playerTr.position + cameraOffset, percent);

            yield return null;
        }
        onCamWarpFinish?.Invoke();
        StartCoroutine("FollowPlayerCoroutine");
    }


    [SerializeField]
    private float warpDistanceX = 80f;
    [SerializeField]
    private float warpDistanceZ = 50f;

    [SerializeField]
    private Vector3 cameraOffset = Vector3.zero;

    private Transform playerTr = null;
    

    private RetVoidParamVoidDelegate onCamWarpFinish = null;
}
