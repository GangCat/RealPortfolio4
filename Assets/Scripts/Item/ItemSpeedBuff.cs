using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpeedBuff : ItemBase
{
    public override void Use(GameObject _entity)
    {
        if (_entity.GetComponent<StatusSpeed>() != null)
            _entity.GetComponent<StatusSpeed>().BuffSpeed(changeSpeedRatio, duration);
        Destroy(gameObject);
    }

    [SerializeField]
    private float changeSpeedRatio = 1.0f;
    [SerializeField]
    private float duration = 10.0f;
}
