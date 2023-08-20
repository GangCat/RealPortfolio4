using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum EAttackType { None = -1, Melee, Range}

[System.Serializable]
public struct WeaponSetting
{
    public EAttackType attackType; // 공격 타입

    public float dmg; // 공격력
    public float maxDmg; // 최대 공격력

    public int curAmmo; // 현재 탄약 수
    public int maxAmmo; // 최대 탄약 수

    public float attackRate; // 공격 속도
    public float minAttackRate; // 최소 공격 속도

    public float attackDistance; // 공격 사거리

    public float[] attributeDmgs; // 속성 공격력
    public float maxAttributeDmg; // 최대 속성 공격력

    public bool isAutomaticAttack; // 연속 공격 가능 여부
}