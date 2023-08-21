using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTotalEnemyKillCount : OnlyTextOperateBase
{
    public void UpdateTotalEnemyKillCount(int _totalEnemyKillCnt)
    {
        myText.text = string.Format("죽인 몬스터 수 : {0:D2}", _totalEnemyKillCnt);
    }
}
