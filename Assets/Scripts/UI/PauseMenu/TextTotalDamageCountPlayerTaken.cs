using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTotalDamageCountPlayerTaken : OnlyTextOperateBase
{
    public void UpdateTotalDamagePlayerTaken(int _totalDamageCntPlayerGain)
    {
        myText.text = string.Format("ÇÇ°Ý È½¼ö : {0}È¸", _totalDamageCntPlayerGain);
    }
}
