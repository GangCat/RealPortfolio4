using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTotalGoldPlayerGain : OnlyTextOperateBase
{
    public void UpdateTotalGoldPlayerGain(int _totalGoldGain)
    {
        myText.text = string.Format("�� ȹ�� ��� : {0:N0}", _totalGoldGain);
    }
}
