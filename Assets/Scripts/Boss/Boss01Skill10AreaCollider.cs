using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss01Skill10AreaCollider : BossTickDamageSkillAreaCollider
{
    public override void OnAttack()
    {
        base.OnAttack();
        pullCollider.GetComponent<Collider>().enabled = true;
    }


    protected override void Awake()
    {
        base.Awake();
        pullCollider = GetComponentInChildren<Boss01Skill10PullAreaCollider>();
    }

    protected override IEnumerator AutoDeActivate()
    {
        yield return new WaitForSeconds(DeactivateTime);

        myCollider.enabled = false;
        pullCollider.GetComponent<Collider>().enabled = false;
        gameObject.SetActive(false);
    }

    private Boss01Skill10PullAreaCollider pullCollider = null;
}
