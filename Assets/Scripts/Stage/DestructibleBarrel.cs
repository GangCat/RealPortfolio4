using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestructibleBarrel : DestructibleObject
{
    public override void TakeDmg(float _dmg)
    {
        if (statusHp.DecreaseHp(_dmg))
        {
            Vector3 spawnPos = transform.position;
            spawnPos.y += 1f;
            Instantiate(ItemPrefabs[Random.Range(0,ItemPrefabs.Length)], spawnPos, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    [SerializeField]
    private GameObject[] ItemPrefabs;
}
