using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossTickDamageSkillAreaCollider : BossAttackAreaCollider
{
    protected virtual void Update()
    {
        lastDamageTime += Time.deltaTime;
    }

    protected virtual void OnEnable()
    {
        lastDamageTime = TickDelayTime;
    }

    protected override void OnTriggerEnter(Collider _other)
    {

    }

    protected virtual void OnTriggerStay(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            if (lastDamageTime >= TickDelayTime)
            {
                lastDamageTime = 0.0f;
                _other.GetComponent<PlayerCollider>().TakeDmg(dmg);
            }
        }
    }


    [SerializeField]
    private float TickDelayTime = 0.5f;

    private float lastDamageTime = 0.0f;
}
