using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextScore : OnlyTextOperateBase
{
    public void UpdateScore(int _score)
    {
        myText.text = string.Format("Score: {0:N0}", _score);
    }
}
