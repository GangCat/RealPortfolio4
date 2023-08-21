using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class OnlyTextOperateBase : MonoBehaviour
{

    protected void Awake()
    {
        myText = GetComponent<TMP_Text>();
    }

    protected TMP_Text myText = null;
}
