using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss03Attack10AreaCollider : BossAttackAreaCollider
{
    private void OnEnable()
    {
        Invoke("PrintWarnArea", 2.9f);
    }

    private void PrintWarnArea()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }

    private void Update()
    {
        if(GetComponent<SpriteRenderer>().enabled == false)
            GetComponentInParent<BossFSM>().Rotate();
    }

    private void OnDisable()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
