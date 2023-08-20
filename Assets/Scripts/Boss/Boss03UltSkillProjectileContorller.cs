using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss03UltSkillProjectileContorller : MonoBehaviour
{
    public void Setup(float _dmg)
    {
        dmg = _dmg;
        SpawnAttackArea();
    }

    private void SpawnAttackArea()
    {
        Vector3 spawnPos = transform.position;
        spawnPos.y = -0.45f;
        ultSkillAreaGo = Instantiate(ultSkillAreaPrefab, spawnPos, Quaternion.Euler(Vector3.right * 90));
        ultSkillAreaGo.GetComponent<BossAttackAreaCollider>().Setup(dmg);
        ultSkillAreaGo.SetActive(true);
    }

    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision.transform.CompareTag("Floor"))
        {
            ultSkillAreaGo.GetComponent<BossAttackAreaCollider>().OnAttack();
            Destroy(gameObject);
        }
    }


    [SerializeField]
    private GameObject ultSkillAreaPrefab;

    private GameObject ultSkillAreaGo = null;

    private float dmg;
}
