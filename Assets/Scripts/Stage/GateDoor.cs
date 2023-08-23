using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateDoor : MonoBehaviour
{
    public void Init()
    {
        oriPos = transform.localPosition;
    }

    public void OpenGateDoor()
    {
        StartCoroutine("OpenGateCoroutine");
    }

    private IEnumerator OpenGateCoroutine()
    {
        Vector3 targetPos = transform.localPosition;
        targetPos.z -= 2f;
        float curTime = Time.time;
        while (Time.time - curTime < 1)
        {
            transform.localPosition = Vector3.Slerp(oriPos, targetPos, Time.time - curTime);

            yield return null;
        }
    }

    public void CloseGateDoor()
    {
        StopCoroutine("OpenGateCoroutine");
        transform.localPosition = oriPos;
    }

    private Vector3 oriPos = Vector3.zero;
}
