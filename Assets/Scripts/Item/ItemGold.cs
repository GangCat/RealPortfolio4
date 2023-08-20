using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGold : ItemBase
{
    public override void Use(GameObject _entity)
    {
        if (_entity.GetComponent<StatusGold>() != null)
            _entity.GetComponent<StatusGold>().IncreaseGold(increaseGold);
        Destroy(gameObject);
    }

    [SerializeField]
    private int increaseGold;
}
