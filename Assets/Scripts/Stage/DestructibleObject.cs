using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatusHP))]
public abstract class DestructibleObject : MonoBehaviour
{
    private void Awake()
    {
        statusHp = GetComponent<StatusHP>();
    }

    public abstract void TakeDmg(float _dmg);

    protected StatusHP statusHp;
}