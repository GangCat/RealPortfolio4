using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimatorController : AnimatorControllerBase
{
    /// <summary>
    /// 해당 이름의 파라미터의 int값을 변경한다.
    /// </summary>
    /// <param name="_paramName"></param>
    /// <param name="_intVal"></param>
    public void SetInteger(string _paramName, int _intVal)
    {
        anim.SetInteger(_paramName, _intVal);
    }

    public void WarnNormalAttack(int _attackType)
    {
        if (_attackType < 10)
            bossArea.CloseNormalAttacks[_attackType].gameObject.SetActive(true);
        else
            bossArea.LongNormalAttacks[_attackType % 10].gameObject.SetActive(true);
    }

    public void WarnSkiilAttack(int _skillType)
    {
        if (_skillType < 10)
            bossArea.CloseSkillAttacks[_skillType].gameObject.SetActive(true);
        else
            bossArea.LongSkillAttacks[_skillType % 10].gameObject.SetActive(true);
    }

    public void WarnUltSkillAttack()
    {
        bossArea.UltSkillAttack.gameObject.SetActive(true);
    }


    public void OnNoramlAttack(int _attackType)
    {
        if (_attackType < 10)
            bossArea.CloseNormalAttacks[_attackType].OnAttack();
        else
            bossArea.LongNormalAttacks[_attackType % 10].OnAttack();
    }

    public void OnSkiilAttack(int _skillType)
    {
        if (_skillType < 10)
            bossArea.CloseSkillAttacks[_skillType].OnAttack();
        else
            bossArea.LongSkillAttacks[_skillType % 10].OnAttack();
    }

    public void OnUltSkillAttack()
    {
        bossArea.UltSkillAttack.OnAttack();
    }

    protected override void Awake()
    {
        base.Awake();
        bossArea = GetComponentInParent<BossAttackAreaSetting>();
    }

    private BossAttackAreaSetting bossArea = null;
}