using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SCrystalInfo
{
    /// <summary>
    /// increase: 증가량, 합연산
    /// ratio: 감소율, 곱연산. 0.2이면 20% 감소시킨다는 의미.
    /// </summary>
    public Sprite mySprite;
    public ECrystalCategory myCategory;
    public ECrystalColor myColor;
    public float increaseAttackDmg;
    public float ratioAttackRate;
    public float increaseSkillDmg;
    public float ratioSkillRate;
    public float increaseMaxHp;
    public float ratioMoveSpeed;
    public float increaseDefense;
    public float increaseAttributeDmg;
    public float increaseAttributeDefense;
    public int myRank;
    [TextArea]
    public string itemInfo;
    [TextArea]
    public string itemStatus; 
}
