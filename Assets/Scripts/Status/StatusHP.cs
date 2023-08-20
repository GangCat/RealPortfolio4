using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HPEvent : UnityEngine.Events.UnityEvent<float, float> { }
public class MaxHpEvent : UnityEngine.Events.UnityEvent<float> { }
public class StatusHP : MonoBehaviour
{
    [HideInInspector]
    public HPEvent onHPEvent = new HPEvent();
    public MaxHpEvent onMaxHpEvent = new MaxHpEvent();
    public float CurHP => curHp;
    public float MaxHP => maxHp;

    public bool DecreaseHp(float _dmg)
    {
        // 공격 받기 전 HP
        float prevHP = curHp;

        if(statusDefense != null)
            _dmg = statusDefense.DefenseDmg(_dmg);

        //float defense = 

        curHp -= (_dmg > 0 ? _dmg : 0);


        Debug.Log(this.name + curHp + "/" + maxHp);

        // 죽었으면 true 반환
        if (curHp <= 0)
        {
            curHp = 0;
            onHPEvent.Invoke(prevHP, curHp);
            return true;
        }

        onHPEvent.Invoke(prevHP, curHp);

        return false;
    }

    public void IncreaseHp(float _HP)
    {
        float prevHP = curHp;

        curHp = curHp + _HP > maxHp ? maxHp : curHp + _HP;

        onHPEvent.Invoke(prevHP, curHp);
    }

    public void ChangeMaxHp(float _increaseHp)
    {
        maxHp = oriMaxHp + _increaseHp;

        onMaxHpEvent.Invoke(maxHp);
        onHPEvent.Invoke(0, curHp);
    }

    public float GetRatio()
    {
        return curHp / maxHp;
    }

    private void OnEnable()
    {
        curHp = maxHp;
    }

    private void Start()
    {
        oriMaxHp = maxHp;
    }

    private void Awake()
    {
        statusDefense = GetComponent<StatusDefense>();
    }

    [Header("-HP")]
    [SerializeField]
    private float curHp = 100;
    [SerializeField]
    private float maxHp = 200;

    private StatusDefense statusDefense;

    private float oriMaxHp;
}
