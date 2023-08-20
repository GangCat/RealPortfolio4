using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackAreaCollider : MonoBehaviour
{
    public virtual void Setup(float _dmg)
    {
        dmg = _dmg;
        gameObject.SetActive(false);
    }

    public virtual void OnAttack()
    {
        myCollider.enabled = true;
        StartCoroutine("AutoDeActivate");
    }

    protected virtual IEnumerator AutoDeActivate()
    {
        yield return new WaitForSeconds(DeactivateTime);

        myCollider.enabled = false;
        gameObject.SetActive(false);
    }

    protected virtual void Awake()
    {
        myCollider = GetComponent<Collider>();
    }

    protected virtual void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            _other.GetComponent<PlayerCollider>().TakeDmg(dmg);
        }
        myCollider.enabled = false;
        gameObject.SetActive(false);
    }


    [SerializeField]
    protected float DeactivateTime = 0.1f;

    protected Collider myCollider = null;

    protected float dmg = 0.0f;
}
