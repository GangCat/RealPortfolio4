using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTotalDamageCountPlayerTaken : OnlyTextOperateBase
{
    public void UpdateTotalDamagePlayerTaken(int _totalDamageCntPlayerGain)
    {
        myText.text = string.Format("�ǰ� Ƚ�� : {0}ȸ", _totalDamageCntPlayerGain);
    }
}
