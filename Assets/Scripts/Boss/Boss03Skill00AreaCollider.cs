using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss03Skill00AreaCollider : BossAttackAreaCollider
{
    public override void Setup(float _dmg)
    {
        dmg = _dmg;
        foreach (BossAttackAreaCollider col in myColliders)
            col.Setup(dmg);
    }

    private void OnEnable()
    {
        StartCoroutine("OnColliderActive");
        StartCoroutine("AutoDeActivate");
    }

    private IEnumerator OnColliderActive()
    {
        myColliders[0].gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);

        myColliders[1].gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);

        myColliders[2].gameObject.SetActive(true);
    }

    public override void OnAttack()
    {
        if (attackCount <= 0)
        {
            myColliders[0].OnAttack();
            ++attackCount;
        }
        else if (attackCount <= 1)
        {
            myColliders[1].OnAttack();
            ++attackCount;
        }
        else
        {
            myColliders[2].OnAttack();
            attackCount = 0;
        }
    }

    protected override IEnumerator AutoDeActivate()
    {
        yield return new WaitForSeconds(DeactivateTime);

        gameObject.SetActive(false);
    }

    protected override void Awake()
    {
    }


    private uint attackCount = 0;

    [SerializeField]
    private BossAttackAreaCollider[] myColliders;
}
