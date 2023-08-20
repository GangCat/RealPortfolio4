using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    public void Setup(float _dmg)
    {
        dmg = _dmg;
    }

    public void Attack(float _attackTime)
    {
        StartCoroutine("AttackFinish", _attackTime);
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
    }

    private IEnumerator AttackFinish(float _attackTime)
    {
        float curTime = Time.time;
        while (Time.time - curTime < 0.2f)
        {
            if (isPaused)
                curTime += Time.deltaTime;

            yield return null;
        }

        myCollider.enabled = true;

        curTime = Time.time;
        while (Time.time - curTime < _attackTime)
        {
            if (isPaused)
                curTime += Time.deltaTime;

            yield return null;
        }

        myCollider.enabled = false;
    }

    private void Awake()
    {
        myCollider = GetComponent<Collider>();
    }

    private void Start()
    {
        myCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            _other.GetComponent<PlayerCollider>().TakeDmg(dmg);
            myCollider.enabled = false;
        }
    }

    private float dmg = 0;

    private bool isPaused = false;

    private Collider myCollider = null;
}
