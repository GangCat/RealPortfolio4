using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTotalUsedAmmoCount : OnlyTextOperateBase
{
    public void UpdateTotalUsedAmmoCount(int _totalCntUsedAmmo)
    {
        myText.text = string.Format("����� �Ѿ� �� : {0:D3}", _totalCntUsedAmmo);
    }
}
