using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollider : MonoBehaviour
{
    public void TakeDmg(float _dmg)
    {
        if (statusHp.DecreaseHp(_dmg))
        {
            Debug.Log(gameObject.name);
            bossFsm.ChangeState(BossFSM.EBossState.Dead);
        }
    }

    [SerializeField]
    private StatusHP statusHp = null;
    [SerializeField]
    private BossFSM bossFsm = null;
}
