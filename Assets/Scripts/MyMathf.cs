using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMathf : MonoBehaviour
{
    public static float CalcAngleToTarget(Vector3 _oriPos, Vector3 _targetPos)
    {
        Vector3 oriPos = _oriPos;
        oriPos.y = 0f;
        Vector3 targetPos = _targetPos;
        targetPos.y = 0f;

        Vector3 dirToTarget = (targetPos - oriPos).normalized;
        return Mathf.Atan2(dirToTarget.z, dirToTarget.x) * Mathf.Rad2Deg;
    }

    public static float CalcAngleToTarget(Vector3 _dirVector)
    {
        return Mathf.Atan2(_dirVector.z, _dirVector.x) * Mathf.Rad2Deg;
    }

    public static void SetRotation(Transform _tr, Vector3 _euler)
    {
        _tr.rotation = Quaternion.Euler(_euler);
    }

    /// <summary>
    /// y�� ȸ���� �� �� ����ϴ� �Լ�
    /// ȸ���� ����� trsnform, ȸ���� ������ �Ű������� �޴´�.
    /// </summary>
    /// <param name="_tr"></param>
    /// <param name="_angleDeg"></param>
    public static void RotateYaw(Transform _tr, float _angleDeg)
    {
        _tr.rotation = Quaternion.Euler(0f, -_angleDeg + 90f, 0f);
    }

    /// <summary>
    /// ���� ���� ���� int���� �����ϴ��� Ȯ���Ѵ�.
    /// �ּ� <= Ÿ�� < �ִ�
    /// </summary>
    /// <param name="_targetInt"></param>
    /// <param name="_minRange"></param>
    /// <param name="_maxRange"></param>
    /// <returns></returns>
    public static bool CheckRange(int _targetInt, int _minRange, int _maxRange)
    {
        if (_minRange > _maxRange)
        {
            Debug.LogError("Range Parameter Error!!");
            Debug.Break();
        }

        if (_targetInt >= _minRange)
        {
            if (_targetInt < _maxRange)
                return true;
        }
        return false;
    }

    /// <summary>
    /// ���� ���� ���� float���� �����ϴ��� Ȯ���Ѵ�.
    /// �ּ� <= Ÿ�� <= �ִ�
    /// </summary>
    /// <param name="_targetFloat"></param>
    /// <param name="_minRange"></param>
    /// <param name="_maxRange"></param>
    /// <returns></returns>
    public static bool CheckRange(float _targetFloat, float _minRange, float _maxRange)
    {
        if (_minRange > _maxRange)
        {
            Debug.LogError("Range Parameter Error!!");
            Debug.Break();
        }

        if (_targetFloat >= _minRange)
        {
            if (_targetFloat <= _maxRange)
                return true;
        }
        return false;
    }

}
