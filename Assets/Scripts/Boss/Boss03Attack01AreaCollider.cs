using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss03Attack01AreaCollider : BossAttackAreaCollider
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
    }

    private IEnumerator OnColliderActive()
    {
        myColliders[0].gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);

        myColliders[1].gameObject.SetActive(true);
    }

    public override void OnAttack()
    {
        if (isFirstCall)
        {
            myColliders[0].OnAttack();
            isFirstCall = false;
        }
        else
        {
            myColliders[1].OnAttack();
            isFirstCall = true;
        }
        
        StartCoroutine("AutoDeActivate");
    }

    protected override IEnumerator AutoDeActivate()
    {
        yield return new WaitForSeconds(DeactivateTime);

        gameObject.SetActive(false);
    }

    protected override void Awake()
    {
    }


    private bool isFirstCall = true;

    [SerializeField]
    private BossAttackAreaCollider[] myColliders;
}
