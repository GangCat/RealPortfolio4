using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusGold : MonoBehaviour
{
    public OnGoldChangeDelegate OnGoldChangeCallback
    {
        set 
        { 
            onGoldChangeCallback = value;
            SetInitGold();
        }
    }

    private void SetInitGold()
    {
        onGoldChangeCallback?.Invoke(curGold);
    }

    public void IncreaseGold(int _gold)
    {
        curGold += _gold;

        if (curGold > maxGold)
        {
            onGoldChangeCallback?.Invoke(_gold - (curGold - maxGold));
            curGold = maxGold;
        }
        else
            onGoldChangeCallback?.Invoke(_gold);
    }

    /// <summary>
    /// �������� �Ű�������ŭ ��µ� �������� �����ϸ� false ��ȯ
    /// </summary>
    /// <param name="_gold"></param>
    /// <returns></returns>
    public bool DecreaseGold(int _gold)
    {
        if (curGold < _gold)
            return false;
        else
        {
            curGold -= _gold;
            return true;
        }
    }

    private void OnEnable()
    {
        curGold = 1000;
    }

    [SerializeField]
    private int curGold = 100;
    [SerializeField]
    private int maxGold = 99999;

    private OnGoldChangeDelegate onGoldChangeCallback = null;
}
