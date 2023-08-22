using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCam : MonoBehaviour
{
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void WarpCamera(Vector3 _warpDir)
    {
        if (_warpDir.x != 0)
            StartCoroutine(WarpCameraCoroutine(transform.position + (_warpDir * warpDistanceX)));
        else
            StartCoroutine(WarpCameraCoroutine(transform.position + (_warpDir * warpDistanceZ)));
    }

    private IEnumerator WarpCameraCoroutine(Vector3 _destPos)
    {
        while (Vector3.SqrMagnitude(transform.position - _destPos) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, _destPos, 0.3f);
            yield return null;
        }
        transform.position = _destPos;
    }


    [SerializeField]
    private float warpDistanceX = 80f;
    [SerializeField]
    private float warpDistanceZ = 50f;
}
