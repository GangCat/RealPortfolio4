using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SCrystalInfo
{
    /// <summary>
    /// increase: ������, �տ���
    /// ratio: ������, ������. 0.2�̸� 20% ���ҽ�Ų�ٴ� �ǹ�.
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
