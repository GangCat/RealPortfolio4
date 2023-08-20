using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextCrystalRank : MonoBehaviour
{
    public void SetRank(int _rank)
    {
        text.text = "LV." + _rank.ToString();
    }

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private TextMeshProUGUI text = null;
}
