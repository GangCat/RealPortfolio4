using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTime : OnlyTextOperateBase
{
    public void UpdateTime(int _min, int _sec)
    {
        myText.text = string.Format("Time: {0:D2}:{1:D2}", _min, _sec);
    }
}
