using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyTickDamageArea : BossTickDamageSkillAreaCollider
{
    public override void Setup(float _dmg)
    {
        dmg = _dmg;
    }


    protected override void OnTriggerEnter(Collider _other)
    {

    }

    protected override void OnEnable()
    {
        base.OnEnable();
        GetComponent<Collider>().enabled = true;
    }
}
