using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public void Init(Transform _targetTr, RetVoidParamVoidDelegate _bossDeadCallback)
    {
        bossFsm = GetComponent<BossFSM>();
        bossFsm.Init(_targetTr, _bossDeadCallback);
    }

    public Vector3 GetLocalPosition()
    {
        return transform.localPosition;
    }

    public void SetLocalPosition(Vector3 _bossPos)
    {
        transform.localPosition = _bossPos;
    }

    public Quaternion GetLocalRotation()
    {
        return transform.localRotation;
    }

    public void SetLocalRotation(Quaternion _bossRot)
    {
        transform.localRotation = _bossRot;
    }


    private BossFSM bossFsm = null;
}
