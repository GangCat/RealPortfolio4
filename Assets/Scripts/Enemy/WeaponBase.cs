using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public float AttackDistance => weaponSetting.attackDistance;
    public float AttackRate => weaponSetting.attackRate;
    public int CurAmmo => weaponSetting.curAmmo;
    public float LimitDistance => limitDistance;

    public Transform TargetTr 
    { 
        set => targetTr = value; 
    }

    public EAttackType AttackType => weaponSetting.attackType;

    public abstract void OnAttack();
    public abstract void Reload();

    public abstract void TogglePause();

    [SerializeField]
    protected WeaponSetting weaponSetting;

    protected Transform targetTr;
    protected float limitDistance;
}
