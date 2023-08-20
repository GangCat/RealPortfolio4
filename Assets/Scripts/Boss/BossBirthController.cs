using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BossBirthController : MonoBehaviour
{
    private void Update()
    {
        if (bossTrs[bossIdx].GetComponentInChildren<BossAnimatorController>().CurAnimationIs("Empty"))
        {
            if(bossIdx + 1 < bossTrs.Length)
            bossTrs[bossIdx + 1].position = bossTrs[bossIdx].position;
            bossTrs[bossIdx + 1].rotation = bossTrs[bossIdx].rotation;

            bossTrs[bossIdx].gameObject.SetActive(false);
            bossTrs[bossIdx + 1].gameObject.SetActive(true);

            ++bossIdx;
        }
    }


    [SerializeField]
    private Transform[] bossTrs;

    private int bossIdx = 0;
}
