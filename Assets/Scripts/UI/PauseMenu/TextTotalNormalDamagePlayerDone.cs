using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTotalNormalDamagePlayerDone : OnlyTextOperateBase
{
    public void UpdateTotalNormalDamagePlayerDone(int _totalNormalDamagePlayerDone)
    {
        myText.text = string.Format("적에게 가한 총 데미지: {0:N0}", _totalNormalDamagePlayerDone);
    }
}
