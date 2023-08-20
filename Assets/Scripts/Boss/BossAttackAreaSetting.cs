using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackAreaSetting : MonoBehaviour
{
    public BossAttackAreaCollider[] CloseNormalAttacks => closeRangeNormalAttackAreaColliders;
    public BossAttackAreaCollider[] LongNormalAttacks => longRangeNormalAttackAreaColliders;
    public BossAttackAreaCollider[] CloseSkillAttacks => closeRangeSkillAttackAreaColliders;
    public BossAttackAreaCollider[] LongSkillAttacks => longRangeSkillAttackAreaColliders;
    public BossAttackAreaCollider UltSkillAttack => UltimateSkillAttackAreaCollider;


    private void Start()
    {
        int i = 0;
        for (i = 0; i < closeRangeNormalAttackAreaColliders.Length; ++i)
            closeRangeNormalAttackAreaColliders[i].Setup(closeRangeAttackDamages[i]);

        for (i = 0; i < longRangeNormalAttackAreaColliders.Length; ++i)
            longRangeNormalAttackAreaColliders[i].Setup(longRangeAttackDamages[i]);

        for (i = 0; i < closeRangeSkillAttackAreaColliders.Length; ++i)
            closeRangeSkillAttackAreaColliders[i].Setup(closeRangeSkillDamages[i]);

        for (i = 0; i < longRangeSkillAttackAreaColliders.Length; ++i)
            longRangeSkillAttackAreaColliders[i].Setup(longRangeSkillDamages[i]);

        if(UltimateSkillAttackAreaCollider != null)
            UltimateSkillAttackAreaCollider.Setup(UltimateSkillDamage);
    }



    [Header("-Boss Attack Damege")]
    [SerializeField]
    private float[] closeRangeAttackDamages;
    [SerializeField]
    private float[] longRangeAttackDamages;
    [SerializeField]
    private float[] closeRangeSkillDamages;
    [SerializeField]
    private float[] longRangeSkillDamages;
    [SerializeField]
    private float UltimateSkillDamage;

    [Header("-Attack Area Colliders")]
    [SerializeField]
    private BossAttackAreaCollider[] closeRangeNormalAttackAreaColliders;
    [SerializeField]
    private BossAttackAreaCollider[] longRangeNormalAttackAreaColliders;
    [SerializeField]
    private BossAttackAreaCollider[] closeRangeSkillAttackAreaColliders;
    [SerializeField]
    private BossAttackAreaCollider[] longRangeSkillAttackAreaColliders;
    [SerializeField]
    private BossAttackAreaCollider UltimateSkillAttackAreaCollider;
}
