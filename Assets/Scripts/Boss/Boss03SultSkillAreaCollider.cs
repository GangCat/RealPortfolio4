using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss03SultSkillAreaCollider : BossAttackAreaCollider
{
    private void OnEnable()
    {
        StartCoroutine("InstantiateMissile");
    }

    private IEnumerator InstantiateMissile()
    {
        int missileCnt = 0;
        while (missileCnt < missileMaxCount)
        {
            yield return new WaitForSeconds(missileSpawnDelay);

            float x = Random.Range(maxSpawnPosTr.position.x, minSpawnPosTr.position.x);
            float z = Random.Range(maxSpawnPosTr.position.z, minSpawnPosTr.position.z);


            GameObject missileGo = Instantiate(bossMissilePrefab, new Vector3(x, y, z), Quaternion.Euler(Vector3.down));
            missileGo.GetComponent<Boss03UltSkillProjectileContorller>().Setup(dmg);
            ++missileCnt;
        }

        gameObject.SetActive(false);
    }


    [SerializeField]
    private GameObject bossMissilePrefab;
    [SerializeField]
    private float missileSpawnDelay;
    [SerializeField]
    private int missileMaxCount;

    [Header("-Boss Room Range")]
    [SerializeField]
    private Transform minSpawnPosTr;
    [SerializeField]
    private Transform maxSpawnPosTr;

    private float y = 20;
}
