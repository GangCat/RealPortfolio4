using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAttackRateBuff : ItemBase
{
    public override void Use(GameObject _entity)
    {
        if (_entity.GetComponentInChildren<WeaponAssaultRifle>() != null)
            _entity.GetComponentInChildren<WeaponAssaultRifle>().BuffAttackRate(changeAttackRateRatio, duration);

        Destroy(gameObject);
    }

    [SerializeField]
    private float changeAttackRateRatio = 1.0f;
    [SerializeField]
    private float duration = 10f;
}
